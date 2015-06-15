//module used for interacting with Trello API
var trelloServer = (function () {
    
    var trelloBoardId = '522fc20a17a1fd7f7b001a44';
    var trelloAppKey = '967d1862f7fd182536edb139eeee8197';
    var trelloToken = '46e66cd37ff14c745e6fc6cc8c337ce15d2cb29657bc44f78c863c5bf0d0c783';
    
    //Display errors in AJAX requests
    $(document).ajaxError(function (event, xhr) {
        alert(xhr.status + ':' + xhr.statusText);
    });

    return {
            getCards: function (boardId) {
                var trelloGetCardsUrl = 'https://api.trello.com/1/boards/' + boardId + '/cards?fields=name,desc,idList,due,shortUrl,idBoard&key=' + trelloAppKey + '&token=' + trelloToken;
                return $.ajax(trelloGetCardsUrl);
            },
            getBoards: function (userId) {
                var trelloGetBoardsUrl = 'https://api.trello.com/1/members/' + userId + '/boards?fields=name&key=' + trelloAppKey + '&token=' + trelloToken;
                return $.ajax(trelloGetBoardsUrl);
            },
            updateCardLabel: function (cardId, newLabel) {
                
                PageMethods.TrelloApiProxy(cardId, newLabel, 'updateLabel');
            },
            updateCardDescription: function (cardId, newDesc) {
            
                PageMethods.TrelloApiProxy(cardId, newDesc, 'updateDescription');
            
            }
        
        };

}());
//module for updating DOM elements
var factory = (function () {

    var templates = {};

    //compile handlebars templates
    var compileTemplates = function () {
        templates.trelloCards = Handlebars.compile($('#trelloCards').html());
        templates.trelloBoards = Handlebars.compile($('#trelloBoards').html());
    };

    //get card data from Trello and display cards
    var showCards = function (data) {
        
        var output = templates.trelloCards({ card: data });
        $('#cardListOutput').html(output);
        $('.draggable:not(.ui-draggable)').draggable({
            cursor: 'move',
            containment: 'document',
            helper: 'clone'
        });
    };

    //get board data from Trell and display drop down list
    var showBoards = function (data) {

        var output = templates.trelloBoards({ board: data });
        $('#boardListOutput').html(output);
    };

    var refreshBoards = function () {
        var userId = $('#txtNickName').val();
        var data = trelloServer.getBoards(userId).done(showBoards);
    };

    var refreshCards = function () {
        var boardId = $('#boardSelect').find(":selected").val()
        trelloServer.getCards(boardId).done(showCards);
    };

    $(function () {
        compileTemplates();
        //refreshCards();

        $('#droppable').droppable({
            activeClass:'dropzone',
            drop: handleDropEvent
        });

        function handleDropEvent(event, ui) {
            var draggable = ui.draggable.html();
            $('#droppable').html(draggable);
            $("#droppable").change();
            utilities.updateDisplay();
        }
        
        $('#updateCard').click(function () {
            
            var cardId = $("#currentCardID").val();
            var newLabel = $("#newCardLabel").val();
            var newDesc = $("#newCardDescription").val();
            trelloServer.updateCardLabel(cardId, newLabel);
            trelloServer.updateCardDescription(cardId, newDesc);
            
        });

        $('#btnGetBoards').click(function () {
            refreshBoards();
        });

    });

    return {
        refreshCards: refreshCards
    };
}());

//module for various utilities that will need to be used throughout the application
var utilities = (function () {

    var updateDisplay = function () {
        
        var currentCardId = $("#droppable").children("div").attr("id");
        var newCardLabel = $("#droppable").find('.cardTitle').html();
        var newCardDescription = $("#droppable").find('.cardDescription').html();
        $("#currentCardID").val(currentCardId);
        $("#newCardLabel").val(newCardLabel);
        $("#newCardDescription").val(newCardDescription);
    };
    
    return {
        updateDisplay: updateDisplay,
    };
}());

//set moment.js formats
var DateStyles = {
    short: "MMMM DD, YYYY",
    long: "dddd DD.MM.YYYY HH:mm"
};

//register moment.js as handlebars helper
Handlebars.registerHelper("formatDate", function (datetime, format) {
    if (moment) {
        f = DateStyles[format];
        return moment(datetime).format(f);
    }
    else {
        return datetime;
    }
});