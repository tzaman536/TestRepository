/*
    NOTE: Verify that URL is correct for every implementation
    TODO: Read URL from app config and set it here

*/

/*
String.Format function
*/
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

// getCartCount(); GET CART COUNT
function getCartCount(){
    //var cartButton = document.getElementById("CartButton");
    var itemsInCart = 0;

    var parms = null;
    $.ajax({
        type: "GET",
        traditional: true,
        // Use following url format if the method is in .cshtml file
        //url: '@Url.Action("GetTotalItemsInCart", "AmzHome")',
        // Use following url format if the method is in .js file
        url: '/AmzHome/GetTotalItemsInCart',
        async: true,
        data: parms,
        dataType: "json",
        beforeSend: function (xhr) {
        },
        success: function (response, textStatus, jqXHR) {
            //alert("Hello");
            itemsInCart = response.Cart.TotalItems;

            $("#shoppingCartLink").text("Cart (" + itemsInCart + ")");
            $("#shoppingCartLink").addClass("cart");
            //cartButton.textContent = "Check Out (" + itemsInCart + ")";

            //var cartLink = document.getElementById("myCartLink");
            //cartLink.textContent = "Check Out (" + itemsInCart + ")";
            //alert(cartLink.className);
            //alert(cartLink.COMMENT_NODE);
            //alert(cartLink.innerHTML);
            //alert(cartLink.TEXT_NODE);




            if (response.Cart.IsValid == false)
                alert(response.Cart.Message);


        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Failed to get cart data. Please reload your page");
        }
    });
}

/*
Add item to cart
Note: Change URL to new clients URL for every implementation 
*/
function addToCart(productID, unitPrice, quantity) {

    var parms = { productID: productID, unitPrice: unitPrice, quantity: quantity }
    $.ajax({
        type: "POST",
        traditional: true,
        //url: '@Url.Action("AddItemToCart", "AmzHome")',
        url: '/AmzHome/AddItemToCart',
        async: true,
        data: parms,
        dataType: "json",
        beforeSend: function (xhr) {
        },
        success: function (response, textStatus, jqXHR) {
            //alert(response.message);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Error found during execution");

        }
    });

    getCartCount();
    var isIE = false || !!document.documentMode;
    if (isIE == true) {
        location.reload();
    }


}


/*
Remove item from cart
Note: Change URL to new clients URL for every implementation 
*/
function removeFromCart(productID) {

    var parms = { productID: productID }
    $.ajax({
        type: "POST",
        traditional: true,
        //url: '@Url.Action("AddItemToCart", "AmzHome")',
        url: '/AmzHome/RemoveItemFromCart',
        async: true,
        data: parms,
        dataType: "json",
        beforeSend: function (xhr) {
        },
        success: function (response, textStatus, jqXHR) {
            //alert(response.message);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Error found during execution");

        }
    });

    getCartCount();
    var isIE = false || !!document.documentMode;
    if (isIE == true) {
        location.reload();
    }


}

