SilverlightRun (SR) is a toolkit for Windows Phone 7 that I use to start off coding faster. SilverlightRun Sample is a demo app that utilizes SR's easy tombstoning features, zooming and view model location. It also works as a basic tutorial.

SR provides its own view model base class ColdViewModel<T> which allows to persist data with SurvivesTombstoning and SurvivesRestart attributes. It also offers a DI approach to view model management with a PhoneTypeCenter that allows generic view model retrieval like App.ViewModels.Get<VM.MainPage>();. The libray uses the "Silverlight for Windows Phone Toolkit" (http://silverlight.codeplex.com/) in order to provide out of the box zooming and panning. There are many more convenient helper classes in this toolkit, to do IS IO, screenshots, dynamic grids and web scraping.

The code is provided as is and without any warranty. I use this toolkit as well and intend to put more stuff in there as I develop a need. To check out the SilverlightRun Sample app, go to http://neokc.de/programme/wp/srs/srs.html or search for it on the marketplace.

Please follow me on twitter: www.twitter.com/halllo
Manuel Naujoks