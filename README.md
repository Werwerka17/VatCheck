# VatCheck

Prosta aplikacja umożliwiająca sprawdzenie podmiotu po numerze NIP przy użyciu API Białej Listy VAT (Ministerstwo Finansów).

## Funkcjonalność

Backend:
- Endpoint: `GET /api/search/nip/{nip}`
- Walidacja formatu NIP (10 cyfr)
- Obsługa przypadków:
  - 400 – nieprawidłowy format NIP
  - 404 – podmiot nie znaleziony
  - 502 – problem po stronie MF
- Zwracany uproszczony model danych:
  - Nazwa
  - NIP
  - Status VAT (czynny/nieczynny)

Frontend:
- Formularz z polem NIP i przyciskiem „Sprawdź”
- Wyświetlenie danych z backendu
- Obsługa stanów:
  - ładowanie
  - błąd
  - brak wyniku


## Technologie

- .NET 8
- ASP.NET Core Web API
- Blazor WebAssembly
- HttpClient
- REST API
- Obsługa statusów HTTP


## Jak uruchomić

1. Otwórz solution w Visual Studio.
2. Ustaw jako projekty startowe:
   - `VatChecker.Api`
   - `VatChecker.Ui`
3. Uruchom aplikację (F5).
4. Frontend otworzy się w przeglądarce.
5. Wpisz numer NIP (10 cyfr) i kliknij „Sprawdź”.
