$(document).ready(function() {
    helloWorld();
    getCardNames();
    getCardSuits();
});

// ******************
// *** Global functions ***
// ******************

function onError(xhr, status, error) {
    console.log(error);
    console.log(xhr.responseText);
    //alert(xhr.responseText);
}

function displayDeckOfCards(msg, cards) {
    var output = msg + ' (' + cards.length + '): ';
    for (var i = 0; i < cards.length; i++) {
        var card = cards[i];
        output += card.Name.Value + card.Suit.Symbol + ' ';
    }
    writeOutput(output.trim());
}

function writeOutput(output) {
    $("#bodyOutput").text(output);
}

function scrollToOutput(){
    var anchor = $("a[name='output']");
    $('html,body').animate(
        { scrollTop: anchor.offset().top }
        ,'slow');
}

// ******************
// *** AJAX Calls ***
// ******************

function helloWorld() {
    var serviceURL = '/Home/HelloWorld';
    $.ajax({
        type: "GET",
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processHelloWorld,
        error: onError
    });
}

function processHelloWorld(data, status) {
    writeOutput(data);
}

function getCardNames() {
    var serviceURL = '/Home/CardNames';
    $.ajax({
        type: "GET",
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: populateCardNames,
        error: onError
    });
}

function populateCardNames(data, status) {
    var selCardNames = $("#selCardNames");
    $.each(data, function() {
        selCardNames.append($("<option />").val(this).text(this));
    });
}

function getCardSuits() {
    var serviceURL = '/Home/CardSuits';
    $.ajax({
        type: "GET",
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: populateCardSuits,
        error: onError
    });
}

function populateCardSuits(data, status) {
    var selCardSuits = $("#selCardSuits");
    $.each(data, function() {
        selCardSuits.append($("<option />").val(this).text(this));
    });
}

function getNewDeckOfCards() {
    var serviceURL = '/Home/CreateDeckOfCards';
    $.ajax({
        type: 'POST',
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processNewDeckOfCards,
        error: onError
    });
}

function processNewDeckOfCards(data, status) {
    scrollToOutput();
    writeOutput('Deck of cards created!');
}


function getDeckOfCards() {
    var serviceURL = '/Home/DeckOfCards';
    $.ajax({
        type: 'GET',
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processDeckOfCards,
        error: onError
    });
}

function processDeckOfCards(data, status) {
    scrollToOutput();
    displayDeckOfCards('Your deck of cards', data.Cards);
}

function shuffleCards() {
    var serviceURL = '/Home/ShuffleCards';
    $.ajax({
        type: 'POST',
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processShuffleCards,
        error: onError
    });
}

function processShuffleCards(data, status) {
    scrollToOutput();
    displayDeckOfCards('Your shuffled cards', data.Cards);
}

function getRandomCard() {
    var serviceURL = '/Home/RandomCard';
    $.ajax({
        type: 'GET',
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processRandomCard,
        error: onError
    });
}

function processRandomCard(data, status) {
    scrollToOutput();
    writeOutput('Your random card: ' + data.Name.Value + data.Suit.Symbol);
}

function sortCards(direction) {
    var serviceURL;
    if (direction == 'asc')
        serviceURL = '/Home/SortCardsAscending';
    else
        serviceURL = '/Home/SortCardsDescending';
    $.ajax({
        type: 'GET',
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processSortedCards,
        error: onError
    });
}

function processSortedCards(data, status) {
    scrollToOutput();
    displayDeckOfCards('Your sorted deck', data.Cards);
}

function filterCards(filter) {
    var serviceURL;
    var jsonData;
    if (filter == 'name') {
        serviceURL = '/Home/FilterDeckByCardName';
        jsonData = { cardName: $("#selCardNames").val() };
    }
    else {
        serviceURL = '/Home/FilterDeckByCardSuit';
        jsonData = { cardSuit: $("#selCardSuits").val() };
    }

    $.ajax({
        type: 'POST',
        url: serviceURL,
        data: JSON.stringify(jsonData),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processFilteredCards,
        error: onError
    });
}

function processFilteredCards(data, status) {
    scrollToOutput();
    displayDeckOfCards('Your filtered deck', data);
}

function addDeck() {
    var serviceURL = '/Home/AddDeck';
    $.ajax({
        type: 'POST',
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processAddDeck,
        error: onError
    });
}

function processAddDeck(data, status) {
    scrollToOutput();
    displayDeckOfCards('Your new deck', data.Cards);
}

function aceUpTheSleeve() {
    var serviceURL = '/Home/AceUpTheSleeve';
    $.ajax({
        type: 'POST',
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processAceUpTheSleeve,
        error: onError
    });
}

function processAceUpTheSleeve(data, status) {
    scrollToOutput();
    writeOutput('It\'s our little secret ;-)');
}

function removeRandomCards() {
    var serviceURL = '/Home/RemoveRandomCards';
    jsonData = { numberOfCardsToRemove: $("#selNumberOfCardsToRemove").val() };
    $.ajax({
        type: 'POST',
        url: serviceURL,
        data: JSON.stringify(jsonData),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processRemoveRandomCards,
        error: onError
    });
}

function processRemoveRandomCards(data, status) {
    scrollToOutput();
    displayDeckOfCards('Card(s) removed', data);
}

function getCardByRank(rank) {
    var serviceURL;
    var jsonData;
    if (rank == 'low') {
        serviceURL = '/Home/LowestRankCard';
    }
    else if (rank == 'high') {
        serviceURL = '/Home/HighestRankCard';
    }

    $.ajax({
        type: 'GET',
        url: serviceURL,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: processCardByRank,
        error: onError
    });
}

function processCardByRank(data, status) {
    scrollToOutput();
    writeOutput('Card identified: ' + data.Name.Value + data.Suit.Symbol);
}

function clearSession() {
    if (confirm('This will remove your card deck from all of existence. Are you sure?')) {
        var serviceURL = '/Home/ClearSession';
        $.ajax({
            type: 'GET',
            url: serviceURL,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: processClearSession,
            error: onError
        });
    }
}

function processClearSession(data, status) {
    scrollToOutput();
    writeOutput("Session cleared!");
}