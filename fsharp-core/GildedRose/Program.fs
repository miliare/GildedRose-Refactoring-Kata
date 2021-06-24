namespace GildedRose

open System.Collections.Generic

type Item = { Name: string; SellIn: int; Quality: int }

type GildedRose(items:IList<Item>) =
    let Items = items

    member this.UpdateQuality() =
        
        let backstagePass = "Backstage passes to a TAFKAL80ETC concert"
        let agedBrie = "Aged Brie"
        let sulfuras = "Sulfuras, Hand of Ragnaros"
        
        let updateSellIn item =
            if item.Name = sulfuras then
                item
            else
                {item with SellIn = item.SellIn - 1}
                
        let updateBackstagePass item =
            let newQuality =
                match item.SellIn with
                | s when s < 0 -> 0
                | s when s < 5 -> item.Quality + 2 // TODO
                | s when s < 10 -> item.Quality + 1
                | _ -> item.Quality
            {item with Quality = System.Math.Clamp(newQuality, 0, 50)}
        
        for i = 0 to Items.Count - 1 do
            Items.[i] <- updateSellIn Items.[i]
            
            if Items.[i].Name = agedBrie || Items.[i].Name = backstagePass then
               if Items.[i].Quality < 50 then
                    Items.[i] <- { Items.[i] with Quality = (Items.[i].Quality + 1) } 
                    if Items.[i].Name = backstagePass then
                        Items.[i] <- updateBackstagePass Items.[i]
            else if Items.[i].Quality > 0 then
                if Items.[i].Name <> sulfuras then
                    Items.[i] <- { Items.[i] with Quality = (Items.[i].Quality - 1) }
                        
            if Items.[i].SellIn < 0 then
                if Items.[i].Name <> agedBrie then
                    if Items.[i].Name <> backstagePass then
                        if Items.[i].Quality > 0 then
                            if Items.[i].Name <> sulfuras then
                                Items.[i] <- { Items.[i] with Quality   = (Items.[i].Quality  - 1) } 
                    else
                        Items.[i] <- { Items.[i] with Quality   = (Items.[i].Quality  - Items.[i].Quality) } 
                else
                    if Items.[i].Quality < 50 then
                        Items.[i] <- { Items.[i] with Quality   = (Items.[i].Quality + 1) }  
        ()


module Program =
    [<EntryPoint>]
    let main argv =
        printfn "OMGHAI!"
        let Items = new List<Item>()
        Items.Add({Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20})
        Items.Add({Name = "Aged Brie"; SellIn = 2; Quality = 0})
        Items.Add({Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7})
        Items.Add({Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80})
        Items.Add({Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80})
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20})
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49})
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49})
        Items.Add({Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6})

        let app = new GildedRose(Items)
        for i = 0 to 30 do
            printfn "-------- day %d --------" i
            printfn "name, sellIn, quality"
            for j = 0 to Items.Count - 1 do
                 printfn "%s, %d, %d" Items.[j].Name Items.[j].SellIn Items.[j].Quality
            printfn ""
            app.UpdateQuality()
        0 