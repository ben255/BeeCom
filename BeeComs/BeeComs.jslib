mergeInto(LibraryManager.library, {

        onBeeMessage: function (message) {
        var str = UTF8ToString(message);
        console.log("onBeeMessage called with:", str);
        window.handleBeeMessage(str);
    },
});