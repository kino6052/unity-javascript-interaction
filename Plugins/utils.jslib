mergeInto(LibraryManager.library, {
  OnMessage: function (str) {
    if (window.OnMessageCallback) {
      window.OnMessageCallback(Pointer_stringify(str));
    }
  },
});
