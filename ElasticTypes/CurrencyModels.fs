module CurrencyModels

open System


type DigitalCurrencyDaily={ 
                            Id: DateTime;
                            Open: float; 
                            High: float; 
                            Low: float; 
                            Close: float; 
                            Volume: float; 
                            MarketCap: float }
