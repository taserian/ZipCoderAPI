# ZipCoder API

## Description
A simple Web API used to determine the state a zip code *should* be located in.

## Explanation
For the 50 states, the first 3 characters of the zip code are enough to naively determine which state a zip code is located in; I call it `zipCodePrefix`. A list of closed intervals `[start, end]` is what I used to store it for use in the Web API. Most states have only one interval, but some states (DC, Georgia, etc.) have multiple intervals, since their zip codes aren't continuous. 

### Interval storage
The SQLite file `StateZipCode.sqlite` contains the base layout for storing the zip code intervals, across tables State and StateZipCodeIntervals. The query

    SELECT State.name, State.abbreviation,   StateZipCodeIntervals.start, StateZipCodeIntervals.end
    from State
    LEFT JOIN StateZipCodeIntervals
    ON State.id = StateZipCodeIntervals.stateId
    
displays these intervals succintly.

### Modules
With 50 states (`n`) and 1000 records (`r`), I came up with three methods for matching zip codes to states :
* `StateIntervalMatcher`, which goes through the list of intervals to determine if the zipCodePrefix is contained in each states' intervals. [search time: `O(n * r)`, space `O(n)` ]
* `StateIndexMatcher`, which generates a `dictionary<int, string>` containing the prefix for each possible zip code in the state. [ construction `O(n)`, search time `O(1)`, space `O(n)` (at most 1000 prefix will be stored)]
* `StateIntervalTreeMatcher`, which generates an interval tree. [construction `O(n log n)`, search time `O(log n)`, space `O(n)`]

In the end, I decided to use the `StateIntervalTreeMatcher`, though the others remain available for the controller to use.

### Data
Even though the problem asked to pull data from one source, I went with a mechanism to pull from multiple sources. The Excel sheet had to be converted to a CSV, since I couldn't find an .xlsx reader that was compatible with `netcoreapp1.1`. However, it can be referred to with the route xls.

### Controllers
The only controller used is `ZipController`, and the route used will define the source for the data, and optionally the state to pull records from.

### Calling the API
*Assuming the Web API is running on localhost:5000*

* `http://localhost:5000/api/zip/{dataSource}` returns a list of states and the number of records for each state for the data source selected.
 Valid values for `dataSource`: [`csv`, `xls`, `xml`]*
* `http://localhost:5000/api/zip/{dataSource}/{stateAbbr}` returns all records for the indicated state for the data source selected.

#### Example API Calls
* `http://localhost:5000/api/zip/csv` for the CSV state list.
* `http://localhost:5000/api/zip/xml/sc` for the XML records for South Carolina.
