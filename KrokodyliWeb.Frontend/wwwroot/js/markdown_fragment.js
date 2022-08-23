
window.markdown_fragment = {
    beautify_ols: function (root) {
        root.find('ol').addClass("list-group list-group-numbered text-white")
    },

    make_windows_clickable: function (root, clickator) {
        root.find('img').each(function () {
            const self = $(this);
            clickator.invokeMethodAsync('MakeCallback', self.attr('src'), self.attr('alt'))
                .then(function (callback) {
                    self.click(function () {
                        callback.invokeMethodAsync('Click');
                    });
                });
        });
    },


    beautify: function (rootRef, clickabilityObj) {
        const root = $(rootRef);
        this.beautify_ols(root);
        clickabilityObj && this.make_windows_clickable(root, clickabilityObj);
    }
};