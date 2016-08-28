# csharpLiDAR
![Logo](https://github.com/assismauro/pyLiDARForest/blob/master/transect1.png)
C Sharp .Net code to access and process LiDAR point clouds

This repository contains C# .Net code that can be used to access LiDAR LAS files. You can find similar Python code [here] (https://github.com/assismauro/pyLiDARForest).

Notes: 

1) Some projects uses [Developer Express libraries](https://www.devexpress.com/). Those are not free, and I decided to use it in a way to improve the user experience. So, if you need to compile all this source code, you should buy a license or download and use the corresponding trial version. I will inform below when it will be necessary to license DevExpress stuff for each application below.

2) You can download executable (ready to use) versions of these applications using links indicate below as well.

We already have four applications:

DigitacaoInventario: application to be used to store and validate tree field data collected in experimental parcels. Its interface is in Portuguese because it was projected to support data acquisition in Amazon, but of course, we can provide different idioms if you wish, just contact me. It uses DevExpress stuff.

Download DigitacaoInventario link: https://mega.nz/#!U4lniYKI
Download key: !n2WVbt4R1Lk7lBPb-TI0LWwIJbM2hs-UzoYLoEY2-GQ

LiDARFileInfo: an application that access and shows LAS file information, discrete and full waveform. It uses DevExpress stuff.

Download LiDARFileInfo link: https://mega.nz/#!F8dRHRxT
Download key: !UE3TxtL3xcq9Mawf0Rz7xWsBNhRX8vcFgnXbu6Fooc0

LiDARFileStuff: contains a class, LiDARFile, that implements full access to discrete and full waveform LAS files, supporting at least 1.2 and 1.3 LAS specification versions. It doesn´t require DevExpress or any other library but .Net ones. The other apps use it.

Download LiDARFileStuff link: https://mega.nz/#!o8lmgLIK
Download key: !UUd83WtSjIRBzXTteOyKEaePZOCdXPsjezyjCoiA1so

LiDARGUID: it set´s some parameters in a LiDAR file. It doesn´t require DevExpress.
To check how to use, just run LiDARGUID -h.

Download LiDARGUID link: https://mega.nz/#!po8yhSJI
Download key: !frg-_ElbRZRvGn45rGM_SH3m0L38BC8Ir9QzsygZefw
 
