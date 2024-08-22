connection.on("Recommendations", function (messageResponse) {
    OnlySpinOff($("#spinReco"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultReco").html(htmlMessage);
});

connection.on("CreateBook", function (messageResponse) {
    OnlySpinOff($("#spinBook"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultBook").html(htmlMessage);
});

//same for addchapter
connection.on("AddChapter", function (messageResponse) {
    OnlySpinOff($("#spinChapter"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultChapter").html(htmlMessage);
});



function GetPersona(persona) {
    _persona = persona;
    //Use Jquery to remove active uin class
    $(".list-group-item").removeClass("active");
    //Add active class to the selected persona
    $("#" + persona).addClass("active");


    GetHistory(persona);

    CreateGraph(persona);


}

function GetHistory(persona) {
    //GetViewLoad($("#spinHistory"), $("#HistoryResult"), $("#" + persona).data("url"),);
    getAPI($("#spinHistory"), $("#HistoryResult"), $("#" + persona).data("url"));

}

function GetRecommendations() {
    var d = {
        brand: "Luxor",
        language: $("#language").val(),
        history: $("#HistoryResult").text(),
    }
    postAPIAsync($("#spinReco"), $("#resultReco"), $("#RecoResult").data("url"), d);
}


function CreateBook() {
    var d = {
        brand: "Luxor",
        language: $("#language").val(),
        history: $("#HistoryResult").text(),
        bookType: $("#bookType").val()
    }
    if (d.bookType == "") {
        d.bookType = "Medieval Fantastic";
    }
    postAPIAsync($("#spinBook"), $("#resultBook"), $("#BookResult").data("url"), d);
}


//add chapter
function AddChapter() {
    var d = {
        brand: "Luxor",
        language: $("#language").val(),
        history: $("#HistoryResult").text(),
        lastPurchase: $("#product").val(),
    }
    postAPIAsync($("#spinChapter"), $("#resultChapter"), $("#ChapterResult").data("url"), d);
}

function CreateGraph(id) {


    // Width and height of the SVG container
    const width = 800;
    const height = 800;

    var data;

    switch (id) {
        case "CLIENT":
            data = {
                nodes: [
                    { id: "Sophie", type: "customer" },
                    { id: "Product(400)", type: "product" },
                    { id: "Product(401)", type: "product" },
                    { id: "Product(402)", type: "product" },
                    { id: "Product(403)", type: "product" },
                    { id: "Order(800)", type: "order" },
                    { id: "Order(801)", type: "order" },
                    { id: "OrderLine(8001)", type: "orderline" },
                    { id: "OrderLine(8002)", type: "orderline" },
                    { id: "OrderLine(8011)", type: "orderline" },
                    { id: "PageVisit(700)", type: "pagevisit" },
                    { id: "PageVisit(701)", type: "pagevisit" },
                    { id: "PageVisit(702)", type: "pagevisit" },
                    { id: "PageVisit(703)", type: "pagevisit" },
                    { id: "PageVisit(704)", type: "pagevisit" },
                    { id: "Cart(600)", type: "cart" },
                    { id: "Cart(601)", type: "cart" },
                    { id: "CartItem(601)", type: "cartitem" },
                    { id: "CartItem(602)", type: "cartitem" },
                    { id: "CartItem(603)", type: "cartitem" },
                    { id: "CartItem(604)", type: "cartitem" },
                    { id: "Event(NewYearSale)", type: "event" },
                    { id: "Event(SpringCollectionLaunch)", type: "event" },
                    { id: "Event(LuxuryExpo)", type: "event" },
                    { id: "Review(900)", type: "review" },
                    { id: "Review(901)", type: "review" },
                    { id: "CustomerService(1000)", type: "customerservice" },
                    { id: "CategoryProduct(40)", type: "category" },
                    { id: "CategoryProduct(41)", type: "category" }
                ],
                links: [
                    { source: "Sophie", target: "Order(801)" },
                    { source: "Sophie", target: "Order(800)" },
                    { source: "Sophie", target: "PageVisit(700)" },
                    { source: "Sophie", target: "PageVisit(701)" },
                    { source: "Sophie", target: "PageVisit(702)" },
                    { source: "Sophie", target: "PageVisit(703)" },
                    { source: "Sophie", target: "PageVisit(704)" },
                    { source: "Sophie", target: "Cart(600)" },
                    { source: "Sophie", target: "Cart(601)" },
                    { source: "Sophie", target: "Event(NewYearSale)" },
                    { source: "Sophie", target: "Event(SpringCollectionLaunch)" },
                    { source: "Sophie", target: "Event(LuxuryExpo)" },
                    { source: "Sophie", target: "Review(900)" },
                    { source: "Sophie", target: "Review(901)" },
                    { source: "Sophie", target: "CustomerService(1000)" },
                    { source: "Order(800)", target: "OrderLine(8001)" },
                    { source: "Order(800)", target: "OrderLine(8002)" },
                    { source: "Order(801)", target: "OrderLine(8011)" },
                    { source: "OrderLine(8001)", target: "Product(400)" },
                    { source: "OrderLine(8002)", target: "Product(401)" },
                    { source: "OrderLine(8011)", target: "Product(403)" },
                    { source: "PageVisit(700)", target: "Product(400)" },
                    { source: "PageVisit(701)", target: "Product(401)" },
                    { source: "PageVisit(702)", target: "Product(403)" },
                    { source: "PageVisit(703)", target: "Product(402)" },
                    { source: "PageVisit(704)", target: "Product(400)" },
                    { source: "Cart(600)", target: "CartItem(601)" },
                    { source: "Cart(600)", target: "CartItem(602)" },
                    { source: "Cart(601)", target: "CartItem(603)" },
                    { source: "Cart(601)", target: "CartItem(604)" },
                    { source: "CartItem(601)", target: "Product(400)" },
                    { source: "CartItem(602)", target: "Product(401)" },
                    { source: "CartItem(603)", target: "Product(403)" },
                    { source: "CartItem(604)", target: "Product(402)" },
                    { source: "Product(400)", target: "CategoryProduct(40)" },
                    { source: "Product(401)", target: "CategoryProduct(41)" },
                    { source: "Product(402)", target: "CategoryProduct(41)" },
                    { source: "Product(403)", target: "CategoryProduct(41)" },
                    { source: "Event(NewYearSale)", target: "Product(400)" },
                    { source: "Event(NewYearSale)", target: "Product(401)" },
                    { source: "Event(SpringCollectionLaunch)", target: "Product(403)" },
                    { source: "Event(LuxuryExpo)", target: "Product(400)" }
                ]
            };


            break;
        case "VIC":
            data = {
                nodes: [
                    { id: "Yuki", type: "customer" },
                    { id: "Product(400)", type: "product" },
                    { id: "Product(401)", type: "product" },
                    { id: "Product(402)", type: "product" },
                    { id: "Product(403)", type: "product" },
                    { id: "CategoryProduct(40)", type: "category" },
                    { id: "CategoryProduct(41)", type: "category" },
                    { id: "Order(900)", type: "order" },
                    { id: "Order(901)", type: "order" },
                    { id: "OrderLine(9001)", type: "orderline" },
                    { id: "OrderLine(9002)", type: "orderline" },
                    { id: "OrderLine(9003)", type: "orderline" },
                    { id: "OrderLine(9011)", type: "orderline" },
                    { id: "OrderLine(9012)", type: "orderline" },
                    { id: "PageVisit(800)", type: "pagevisit" },
                    { id: "PageVisit(801)", type: "pagevisit" },
                    { id: "PageVisit(802)", type: "pagevisit" },
                    { id: "PageVisit(803)", type: "pagevisit" },
                    { id: "PageVisit(804)", type: "pagevisit" },
                    { id: "Cart(700)", type: "cart" },
                    { id: "Cart(701)", type: "cart" },
                    { id: "Cart(702)", type: "cart" },
                    { id: "CartItem(701)", type: "cartitem" },
                    { id: "CartItem(702)", type: "cartitem" },
                    { id: "CartItem(703)", type: "cartitem" },
                    { id: "CartItem(704)", type: "cartitem" },
                    { id: "CartItem(705)", type: "cartitem" },
                    { id: "CartItem(706)", type: "cartitem" },
                    { id: "CartItem(707)", type: "cartitem" },
                    { id: "Event(ValentinesDay)", type: "event" },
                    { id: "Event(SpringCollectionLaunch)", type: "event" },
                    { id: "Event(LuxuryExpo)", type: "event" },
                    { id: "Review(902)", type: "review" },
                    { id: "Review(903)", type: "review" },
                    { id: "CustomerService(1001)", type: "customerservice" }
                ],
                links: [
                    { source: "Yuki", target: "Order(900)" },
                    { source: "Yuki", target: "Order(901)" },
                    { source: "Yuki", target: "PageVisit(800)" },
                    { source: "Yuki", target: "PageVisit(801)" },
                    { source: "Yuki", target: "PageVisit(802)" },
                    { source: "Yuki", target: "PageVisit(803)" },
                    { source: "Yuki", target: "PageVisit(804)" },
                    { source: "Yuki", target: "Cart(700)" },
                    { source: "Yuki", target: "Cart(701)" },
                    { source: "Yuki", target: "Cart(702)" },
                    { source: "Yuki", target: "Event(ValentinesDay)" },
                    { source: "Yuki", target: "Event(SpringCollectionLaunch)" },
                    { source: "Yuki", target: "Event(LuxuryExpo)" },
                    { source: "Yuki", target: "Review(902)" },
                    { source: "Yuki", target: "Review(903)" },
                    { source: "Yuki", target: "CustomerService(1001)" },
                    { source: "Order(900)", target: "OrderLine(9001)" },
                    { source: "Order(900)", target: "OrderLine(9002)" },
                    { source: "Order(900)", target: "OrderLine(9003)" },
                    { source: "Order(901)", target: "OrderLine(9011)" },
                    { source: "Order(901)", target: "OrderLine(9012)" },
                    { source: "OrderLine(9001)", target: "Product(400)" },
                    { source: "OrderLine(9002)", target: "Product(401)" },
                    { source: "OrderLine(9003)", target: "Product(402)" },
                    { source: "OrderLine(9011)", target: "Product(403)" },
                    { source: "OrderLine(9012)", target: "Product(402)" },
                    { source: "PageVisit(800)", target: "Product(400)" },
                    { source: "PageVisit(801)", target: "Product(401)" },
                    { source: "PageVisit(802)", target: "Product(402)" },
                    { source: "PageVisit(803)", target: "Product(403)" },
                    { source: "PageVisit(804)", target: "Product(400)" },
                    { source: "Cart(700)", target: "CartItem(701)" },
                    { source: "Cart(700)", target: "CartItem(702)" },
                    { source: "Cart(700)", target: "CartItem(703)" },
                    { source: "Cart(701)", target: "CartItem(704)" },
                    { source: "Cart(701)", target: "CartItem(705)" },
                    { source: "Cart(702)", target: "CartItem(706)" },
                    { source: "Cart(702)", target: "CartItem(707)" },
                    { source: "CartItem(701)", target: "Product(400)" },
                    { source: "CartItem(702)", target: "Product(401)" },
                    { source: "CartItem(703)", target: "Product(402)" },
                    { source: "CartItem(704)", target: "Product(403)" },
                    { source: "CartItem(705)", target: "Product(402)" },
                    { source: "CartItem(706)", target: "Product(400)" },
                    { source: "CartItem(707)", target: "Product(401)" },
                    { source: "Product(400)", target: "CategoryProduct(40)" },
                    { source: "Product(401)", target: "CategoryProduct(41)" },
                    { source: "Product(402)", target: "CategoryProduct(41)" },
                    { source: "Product(403)", target: "CategoryProduct(41)" },
                    { source: "Event(ValentinesDay)", target: "Product(400)" },
                    { source: "Event(SpringCollectionLaunch)", target: "Product(401)" },
                    { source: "Event(SpringCollectionLaunch)", target: "Product(403)" },
                    { source: "Event(LuxuryExpo)", target: "Product(400)" }
                ]
            };



            break;

        case "PROSPECT":

        case "VIC":
            data = {
                nodes: [
                    { id: "John", type: "customer" },
                    { id: "Product(400)", type: "product" },
                    { id: "Product(401)", type: "product" },
                    { id: "Product(402)", type: "product" },
                    { id: "Product(403)", type: "product" },
                    { id: "CategoryProduct(40)", type: "category" },
                    { id: "CategoryProduct(41)", type: "category" },
                    { id: "PageVisit(900)", type: "pagevisit" },
                    { id: "PageVisit(901)", type: "pagevisit" },
                    { id: "PageVisit(902)", type: "pagevisit" },
                    { id: "PageVisit(903)", type: "pagevisit" },
                    { id: "Cart(800)", type: "cart" },
                    { id: "Cart(801)", type: "cart" },
                    { id: "CartItem(801)", type: "cartitem" },
                    { id: "CartItem(802)", type: "cartitem" },
                    { id: "CartItem(803)", type: "cartitem" },
                    { id: "CartItem(804)", type: "cartitem" },
                    { id: "Event(ValentinesDay)", type: "event" },
                    { id: "Event(SpringCollectionLaunch)", type: "event" }
                ],
                links: [
                    { source: "John", target: "PageVisit(900)" },
                    { source: "John", target: "PageVisit(901)" },
                    { source: "John", target: "PageVisit(902)" },
                    { source: "John", target: "PageVisit(903)" },
                    { source: "John", target: "Cart(800)" },
                    { source: "John", target: "Cart(801)" },
                    { source: "John", target: "Event(ValentinesDay)" },
                    { source: "John", target: "Event(SpringCollectionLaunch)" },
                    { source: "PageVisit(900)", target: "Product(400)" },
                    { source: "PageVisit(901)", target: "Product(401)" },
                    { source: "PageVisit(902)", target: "Product(403)" },
                    { source: "PageVisit(903)", target: "Product(402)" },
                    { source: "Cart(800)", target: "CartItem(801)" },
                    { source: "Cart(800)", target: "CartItem(802)" },
                    { source: "Cart(801)", target: "CartItem(803)" },
                    { source: "Cart(801)", target: "CartItem(804)" },
                    { source: "CartItem(801)", target: "Product(400)" },
                    { source: "CartItem(802)", target: "Product(401)" },
                    { source: "CartItem(803)", target: "Product(403)" },
                    { source: "CartItem(804)", target: "Product(402)" },
                    { source: "Product(400)", target: "CategoryProduct(40)" },
                    { source: "Product(401)", target: "CategoryProduct(41)" },
                    { source: "Product(402)", target: "CategoryProduct(41)" },
                    { source: "Product(403)", target: "CategoryProduct(41)" },
                    { source: "Event(ValentinesDay)", target: "Product(400)" },
                    { source: "Event(ValentinesDay)", target: "Product(401)" },
                    { source: "Event(SpringCollectionLaunch)", target: "Product(403)" }
                ]
            };
            break;

    }

    const color = d3.scaleOrdinal()
        .domain(["customer", "product", "order", "orderline", "pagevisit", "cart", "cartitem", "event", "review", "customerservice", "category"])
        .range(["#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd", "#8c564b", "#e377c2", "#7f7f7f", "#bcbd22", "#17becf", "#aec7e8"]);

    d3.select("#graph-container").select("svg").remove();
    const svg = d3.select("#graph-container").append("svg")
        .attr("width", width)
        .attr("height", height);

    const simulation = d3.forceSimulation(data.nodes)
        .force("link", d3.forceLink(data.links).id(d => d.id).distance(100))
        .force("charge", d3.forceManyBody().strength(-300))
        .force("center", d3.forceCenter(width / 2, height / 2))
        .on("tick", ticked);

    const link = svg.append("g")
        .attr("class", "links")
        .selectAll("line")
        .data(data.links)
        .enter().append("line")
        .attr("class", "link");

    const node = svg.append("g")
        .attr("class", "nodes")
        .selectAll("g")
        .data(data.nodes)
        .enter().append("g")
        .attr("class", "node");

    node.append("circle")
        .attr("r", 12)
        .attr("fill", d => color(d.type));

    node.append("text")
        .attr("x", 15)
        .attr("y", ".55em")
        .text(d => d.id);

    function ticked() {
        link
            .attr("x1", d => d.source.x)
            .attr("y1", d => d.source.y)
            .attr("x2", d => d.target.x)
            .attr("y2", d => d.target.y);

        node
            .attr("transform", d => `translate(${d.x},${d.y})`);

    }
}





