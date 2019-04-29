# Assignment4
100 points

For this assignment you will need to create a mapping application.
I have created a starter app for your that is based on a Xamarin Forms tabbed application
It has 2 pages, a generica item list and an about page
I have already added MvvmCross to the app so will not need to.
Your application will need to start with a map page with the user's current location indicated with 
a pin (you can hard code the lat/long for the location). There should be a search bar at the top'
of the screen to allow the user to search on a name.  The search should make a call to the Google Places API
like we did in class.  However, in addition you should also have a picker control on the same page that allows
the user to select the type of item to seach on.  The picker should contain, at a minimum restaurant, lodging, and car_rental.
You can find the available search tyoes here: https://developers.google.com/places/web-service/supported_types
Take the selected type from the picker control and add it as a query parameter to the search url we used in class.
Feel free to use my API key from class to gain access to the API.
The map should "zero in" on the user location with a view radius of 3500 meters.  The search radius should also be 3500 meters or about 2 miles
When the search returns, you should add a pin to the map for each location returned with the name and address displayed in the pin callout.
You should have a service with an interface that gets injected into the mapping view model that performs the search.
Each new search should clear out the previous search results and display the new set of pins on the map.

Happy Coding!
