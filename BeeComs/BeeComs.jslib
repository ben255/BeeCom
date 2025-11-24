mergeInto(LibraryManager.library, {

    onBeeMessage: function (message) {
     window.handleBeeMessage(UTF8ToString(message));
  },
});