# This repository contains some notes on celestial mechanics in a Blazor website. 
The interest stems from 2 properties of the planet Mercury's motion

Mercury's spin orbit coupling i.e. Mercury spins on it axis 3 times for every 2 times it orbits the sun.

Mercury's (or any satellite's) apsidal precession due to the central body's oblateness i.e. the orbital ellipse rotates (i.e. precesses) very slowly each orbit. Normally this precession is discussed alongside General Relativity. However apsidal precession happens whenever any satellite orbits an oblate central body. Note however that General Relativity has a greater effect on apsidal precession than the Sun's oblateness.

This repository includes a PDF to explain the maths behind the above 2 effects. The PDF mainly focuses on Kaula's eccentricity functions and their use in explaining the above. It also includes javascript to demonstrate the precession.

Both can be accessed at www.xetle.com

The website is a Blazor website. Blazor is a Microsoft technology.
The advantage of using Blazor (for me) is that I can use C# to code client side functionality - rather than javascript which I am less familiar with.
The Blazor site is a solely client side application (or WebAssembly) i.e. there is no web server - the files are stored in Github Pages.
