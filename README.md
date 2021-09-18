# Easy-Translate
Easy to use Translation Framework for Mods or other Projects

## Initialize Translation
 *Init(string environement, string locale);*
 
    EasyTranslate.EasyTranslate easyTrans = new EasyTranslate.EasyTranslate();
    easyTrans.Init("ch.easy.develope.vh.diving.mod", "EN_en");

## Setup Locales
 *setLocales(Dictionary<string, string> dict);*

> Define all the locales u want to translate

 
    var locales = new Dictionary<string, string>();
    
    locales.Add("EN", "en");
    locales.Add("DE", "de");
    locales.Add("RU", "ru");
    
    easyTrans.setLocales(locales);

## Setup Translations
 *SetupKeys(string key, string translation);*
 

> This will generate foreach locale a template including your keys and values 
> in your initialized locale (in this example "EN_en)".
> 
> The templates are located at "< Assambly Location >/< environement >/< locale >/translation.txt"
> 
> Key/Value seperator is "=/="
> 
> Go to next key is seperated by "=NL="
> 
> You can use linebreaks in your translation strings.

     var translation = new Dictionary<string, string>();
     
     translation.Add("main_class_title", "Im a title!");
     translation.Add("main_class_btn_txt", "Im a button text");
     translation.Add("main_class_description", "Im on the first line \n Im on the second line");
     
     easyTrans.SetupKeys(translation);


## Get Translation by Key
 *Init(string key, string default_value);*
 

> default_value is by defualt empty and fires when the key was not found. but you can define one by adding a second param.

    easyTrans.getTranslation("main_class_title");


     
