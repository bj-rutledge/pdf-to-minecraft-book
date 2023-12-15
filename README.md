# PDF to Minecraft 

This console application extracts title, author, and text from a PDF file and converts it to a UTF-8 *.stenhal file(s) for consumption by the popular game Minecraft. Stenhal files are what Minecraft uses to create a book object in the game. As well as converting the text to stenhal file format, this application also removes instances of user provided strings from the text in a book. 

I created this console app for my stepson, who loves playing Minecraft. He wanted to create a library of actual books that existed in the real world, but could not add PDF files to his library, only a proprietary utf-8 file format “*.stenhal”. He was laboriously copying/pasting books and converting them to the format when I asked him, does Minecraft have an app/plugin for that? When he answered no, I set out to build him one! So, this simple app does just that and a couple other things to help out. Also, for fun, I created a 'Mystery Box' option, which converts images into ascii art for fun!

Example of Minecraft book: 
```
title: There Will Come Soft RainsBy
author: Ray BradburyIn
pages:
#- In the living room the voice-clock sang, Tick-tock, seven o'clock, time to get up, time to get up,seven o 'clock! as if it were afraid that nobody would. The morning house lay empty. The clockticked on, repeating
#- and repeating its sounds into the emptiness. Seven-nine, breakfast time,seven-nine!In the kitchen the breakfast stove gave a hissing sigh and ejected from its warm interior eight piecesof perfectly browned toast, eight eggs sunny side up, sixteen slices of
#- bacon, two coffees, and twocool glasses of milk."Today is August 4, 2026," said a second voice from the kitchen ceiling, "in the city ofAllendale, California." It repeated the date three times for memory's sake. "Today is Mr.Featherstone's birthday. Today
#- is the anniversary of Tilita's marriage. Insurance is payable,as are the water, gas, and light bills."Somewhere in the walls, relays clicked, memory tapes glided under electric eyes.Eight-one, tick-tock, eight-one o'clock, off to school, off to work, run,
#- run, eight-one! But nodoors slammed, no carpets to
...

```
