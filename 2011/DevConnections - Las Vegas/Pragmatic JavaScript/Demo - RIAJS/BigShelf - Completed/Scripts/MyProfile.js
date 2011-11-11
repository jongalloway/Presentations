/// <reference path="References.js" />

$(document).ready(function () {

    var serviceUrl = 'BigShelf-BigShelfService.svc';

    // Load our "profile" record 
    var profile,
    profileDataSource = $.dataSource({
        serviceUrl: serviceUrl,
        queryName: "GetProfileForProfileUpdate",
        bufferChanges: true,
        refresh: function (profiles) {
            profile = profiles[0];
            render();
        }
    }).refresh();  

    function render() {

        // Data-link "profile" in the <form> and page title, so our UI updates live. 
        $("#profileForm").link(profile);
        $("#profileName").link(profile);

        // Set up UI styling updates in response to changes in the object 
        $(profile).bind("propertyChange", function () {
            // When a property changes on the profile object enable the Cancel button. 
            // If all fields have passed validation, ALSO enable the Save button. 
            updateSaveCancelButtonState();

            // If a property changes, we highlight the changed field in the UI 
            updatePropertyAdornments();
        });

        // When profile leaves its "Unmodified" state, we'll enable the Save/Cancel buttons. 
        // When we complete a save, we'll want to remove our updated scalar property adornments. 
        profileDataSource.option("entityStateChange", function () {
            updatePropertyAdornments();
            updateSaveCancelButtonState();
        });

        // Bind the revert button on each form <input>. 
        $("tr.profile-property.updated span.revertDelete").live("click", function () {
            var propertyName = $(this).siblings("input").attr("name");
            profileDataSource.revertChanges(profile, propertyName);
        });

        // Bind our Save/Cancel buttons. 
        $("#submit").click(function () {
            if ($("#profileForm").valid()) {
                profileDataSource.commitChanges();
            }
        });
        $("#cancel").click(function () { profileDataSource.revertChanges(true); });

        // Set up validation of property fields, validation rules come from 
        // the server where they were extracted from data annotations on the object's type 
        $("#profileForm").validate({
            rules: profileDataSource.getEntityValidationRules().rules,
            errorPlacement: function (error, element) {
                error.appendTo(element.closest("tr"));
            }
        });

        // Now let's implement the friends list which uses associated entities 
        var friends = profile.get_Friends();

        // First, implement loading the friends list and populating a 
        // control with the result 
        var friendsNames = {};
        var friendsList = $("#friend-list").list({
            data: friends,
            template: $("#profile-friend-template"),
            templateOptions: {
                getFriendName: function (friend) {
                    var profile = friend.get_FriendProfile();
                    if (profile) {
                        return profile.Name;
                    } else {
                        return friendsNames[friend.FriendId];
                    }
                }
            }
        }).data("list");

        // Now create an auto-complete over the add-friend-text <input> field 
        // to make it easier to load friends into it 
        $("#add-friend-text").autocomplete({
            source: function (term, callback) {
                var filter = [
            { property: "Name", operator: "Contains", value: term.term },
                // Filter out the profile of the current user 
            {property: "Id", operator: "!=", value: profile.Id }
        ].concat($.map(friends, function (friend) {
            // Filter out existing friends 
            return { property: "Id", operator: "!=", value: friend.FriendId };
        }));

                $.dataSource({
                    serviceUrl: serviceUrl,
                    queryName: "GetProfiles",
                    filter: filter,
                    refresh: function (friendProfiles) {
                        // Transform each result into a label/foreignKey pair 
                        callback($.map(friendProfiles, function (friend) {
                            return { label: friend.Name, foreignKey: friend.Id };
                        }));
                    }
                }).refresh();
            },
            select: function (event, data) {
                // Stow the foreign key value where our "Add Friend" button's click handler can find it. 
                $("#add-friend").data("friendId", data.item.foreignKey);

                // Enable the "Add Friend" button since we have a selected friend to add 
                $("#add-friend-button").enable().focus();
            }
        }).keyup(function (event) {
            // Any keystroke that doesn't select a Friend should disable the "Add Friend" button 
            if (event.keyCode !== 13) {
                $("#add-friend-button").disable();
            }
        }).watermark();

        // Now implement adding a friend once the friend has been populated in 
        // the add-friend-button <input> field 
        $("#add-friend-button").click(function () {
            var friendId = $("#add-friend").data("friendId");
            friendsNames[friendId] = $("#add-friend-text").val();
            $.observable(friends).insert(friends.length, { FriendId: friendId });

            $("#add-friend-text").val("");
            $(this).disable();
        });

        // Update the Submit / Cancel button state when a friend is reverted 
        $([friends]).bind("arrayChange", function () {
            updateSaveCancelButtonState();
        });

        // For each Friend child entity, transitioning from the "Unmodified" entity state 
        // indicates an add/remove.  Such a change should update our per-entity added/removed styling.  
        // It should also enable/disable our Save/Cancel buttons. 
        $([friends]).dataSource().option("entityStateChange", function (entity, state) {
            updateFriendAddDeleteAdornment(entity);
            updateSaveCancelButtonState();
        });

        // Bind revert/delete button on friends. 
        // It's convenient to use "live" here to bind/unbind these handlers as child entities are added/removed. 
        $("#friend-list span.revertDelete").live("click", function () {
            var friendElement = $(this).closest("li");
            var friend = friendsList.dataForNode(friendElement[0]);
            if (friendElement.hasClass("deleted") || friendElement.hasClass("added")) {
                $([friends]).dataSource().revertChanges(friend);
            } else {
                friends.deleteEntity(friend);
            }
        });

        //
        // Helper functions
        //

        function updateSaveCancelButtonState() {
            var haveChanges = hasChanges(profileDataSource) || hasChanges($([friends]).dataSource());
            var changesValid = $("#profileForm").valid();
            $("#submit").toggleEnabled(haveChanges && changesValid);

            // Can cancel changes regardless if they are valid or not
            $("#cancel").toggleEnabled(haveChanges);

            function hasChanges(dataSource) {
                return $.grep(dataSource.getEntities(), function (entity) {
                    switch (dataSource.getEntityState(entity)) {
                        case "ClientUpdated":
                        case "ClientAdded":
                        case "ClientDeleted":
                            return true;

                        case "Unmodified":  // No changes to commit.
                        case "ServerUpdating":  // Commit is in progress, so disable Save/Cancel button.
                        case "ServerAdding":
                        case "ServerDeleting":
                            return false;
                    }
                }).length > 0;
            };
        };

        function updatePropertyAdornments() {
            $("tr.profile-property")
                .removeClass("updated")
                .filter(function () {
                    return isModifiedProfileProperty($(this).find("input").attr("name"));
                })
                .addClass("updated");

            function isModifiedProfileProperty(propertyName) {
                var profileEntityState = profileDataSource.getEntityState(profile)
                switch (profileEntityState) {
                    case "ClientUpdated":  // Profile entity is only updated on the client.
                    case "ServerUpdating":  // Profile entity is updated on the client and sync'ing with server (but unconfirmed).
                        return profileDataSource.isPropertyChanged(profile, propertyName);

                    default:
                        return false;
                }
            };
        };

        function updateFriendAddDeleteAdornment(friend) {
            var entityState = $([friends]).dataSource().getEntityState(friend) || "";
            var isDeleted = entityState === "ClientDeleted" || entityState === "ServerDeleting";
            var isAdded = entityState === "ClientAdded" || entityState === "ServerAdding";

            var friendsListItemElement = friendsList.nodeForData(friend);
            $(friendsListItemElement)
                .toggleClass("deleted", isDeleted)
                .toggleClass("added", isAdded);
        };
    };
});
