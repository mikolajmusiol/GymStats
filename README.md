# GymProject
Projekt mający na celu zbieranie i analizowanie danych z wejść na siłownię.
## Opis projektu 
Projekt jest aplikacją konsolową. Po podaniu w konsoli danych do logowania na stronę siłowni, program zaloguje się przy użyciu narzędzia Selenium i pobierze kod źrodłowy strony zawierający historię wejść na siłownię 
oraz dane użytkownika. Informację z kodu źrodłowego są wyszukiwane przez odpowiednie wyrażenia regularne i przypisywane do zmiennych, za pomocą których poźniej tworzone są obiekty klas i obliczane są statystyki 
każdego użytkownika (np. średni czas trwania treningu, suma wydanych pieniędzy na karnet) jak i ogółu użytkowników. 
Następnie, przy pomocy biblioteki Dapper, aktualizowana jest baza danych Microsoft SQL Server. 
Na końcu program wypisuje w konsoli obliczone statystyki użytkownika wraz z uśrednionymi statystykami wszystkich użytkowników dla porównania.
## Przyszłość projektu
W przyszłości zamierzam rozwinąć projekt o własną stronę internetową z formularzem logowania, na której użytkownik mógłby zalogować się i uzyskać dostęp do swoich statystyk, zamiast wprowadzać dane przez konsolę.
