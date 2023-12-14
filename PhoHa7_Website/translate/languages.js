/*
sourceLanguage: the 2-3 letter language code of the source language (English = "en")
targetLanguage: the 2-3 letter language code of the target language (Hebrew is "iw")
text: the text to translate
callback: the function to call once the request finishes*

* Javascript is much different from C# in that it is an asynchronous language, which 
means it works on a system of events, where anything may happen at any time
(which makes sense when dealing with things on the web like sending requests to a 
server). Because of this, Javascript allows you to pass entire
functions as parameters to other functions (called callbacks) that trigger when some 
time-based event triggers. In this case, as seen below,
we use our callback function when the request to google translate finishes.
*/

const translate = function(sourceLanguage,targetLanguage,text,callback) {
    // make a new HTTP request
  const request = new XMLHttpRequest();

    /*
      when the request finishes, call the specified callback function with the 
      response data
    */
    request.onload = function() {
        // using JSON.parse to turn response text into a JSON object
        callback(JSON.parse(request.responseText));
    }

    /*
      set up HTTP GET request to translate.googleapis.com with the specified language 
      and translation text parameters
    */
    request.open(
      "GET",
      "https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + 
      sourceLanguage + "&tl=" + targetLanguage + "&dt=t&q=" + text,
      true
    );

    // send the request
    request.send();
}



/*
  translate "This shouldn't download anything" from English to Hebrew
  (when the request finishes, it will follow request.onload (specified above) and 
  call the anonymous 
  function we use below with the request response text)
*/

//translate("en","vi","tendon!",function(translation) {
//    // output google's JSON object with the translation to the console
//    console.log(translation);
//});
