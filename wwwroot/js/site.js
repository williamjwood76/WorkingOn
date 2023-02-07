var paychex = paychex || {};
$(() => {
    paychex.ajaxCall = (url, ajaxData, method, dataType, async) => {
        ajaxData = ajaxData || {};
        async = async || true;
        method = method || "POST";
        dataType = dataType || "json";
        if (url === undefined) {
            throw new Error("Error: URL is not defined!");
        }
        return $.ajax({
            type: method,
            async: async,
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: dataType,
            data: JSON.stringify(ajaxData),
            error: function (xhr, status, error) {
                console.log(status, error, xhr.responseText);
            }
        });
    };
    paychex.openPopup = (id) => {
        $.magnificPopup.open({
            items: {
                type: "inline",
                src: id
            },
            showCloseBtn: false,
            closeOnBgClick: false,
            modal: true
        });
    }
});