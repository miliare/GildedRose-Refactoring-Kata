module GildedRose.UnitTests

open GildedRose
open System.Collections.Generic
open Xunit
open Swensen.Unquote

[<Fact>]
let ``Standard items updates`` () =
    let Items = List<Item>()  
    Items.Add({
        Name = "+5 Dexterity Vest"
        SellIn = 1
        Quality = 3})
    let app = GildedRose(Items)
    app.UpdateQuality()
    test <@ "+5 Dexterity Vest" = Items.[0].Name @>
    test <@ 0 = Items.[0].SellIn @>
    test <@ 2 = Items.[0].Quality @>
    app.UpdateQuality()
    test <@ -1 = Items.[0].SellIn @>
    test <@ 0 = Items.[0].Quality @>
    app.UpdateQuality()
    test <@ -2 = Items.[0].SellIn @>
    test <@ 0 = Items.[0].Quality @>

[<Fact>]
let ``Aged Brie item updates`` () =
    let Items = List<Item>()  
    Items.Add({
        Name = "Aged Brie"
        SellIn = 1
        Quality = 47})
    let app = GildedRose(Items)
    app.UpdateQuality()
    test <@ "Aged Brie" = Items.[0].Name @>
    test <@ 0 = Items.[0].SellIn @>
    test <@ 48 = Items.[0].Quality @>
    app.UpdateQuality()
    test <@ -1 = Items.[0].SellIn @>
    test <@ 50 = Items.[0].Quality @>
    app.UpdateQuality()
    test <@ -2 = Items.[0].SellIn @>
    test <@ 50 = Items.[0].Quality @>

[<Fact>]
let ``Sulfuras, Hand of Ragnaros item updates`` () =
    let Items = List<Item>()  
    Items.Add({
        Name = "Sulfuras, Hand of Ragnaros"
        SellIn = 0
        Quality = 80})
    let app = GildedRose(Items)
    app.UpdateQuality()
    test <@ "Sulfuras, Hand of Ragnaros" = Items.[0].Name @>
    test <@ 0 = Items.[0].SellIn @>
    test <@ 80 = Items.[0].Quality @>
    app.UpdateQuality()
    test <@ 0 = Items.[0].SellIn @>
    test <@ 80 = Items.[0].Quality @>