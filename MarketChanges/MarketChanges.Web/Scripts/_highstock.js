$(function myChart(){
    $highcharts('StockChart', {

        chart: {
        },

        xAxis: {
            type: 'datetime',
            dateTimeLabelFormats: {

                year: '%Y'
            },

        },

        rangeSelector: {
            selected: 1,
            buttons: [{
                type: 'year',
                count: 1,
                text: '1y'
            }, {
                type: 'year',
                count: 5,
                text: '5y'
            }, {
                type: 'all',
                text: 'All'
            }]
        },
        /*
         plotOptions: {
            series: {
                pointStart: Date.UTC(1833, 0, 1),
                pointInterval: 24 * 3600 * 1000000 // one day
            }
        },
        */

        series: [{
            name: 'USD to EUR',
            data: [
                [new Date(1831, 1, 1).valueOf(), 1],
                [new Date(1844, 1, 1).valueOf(), 1],
                [new Date(1847, 1, 1).valueOf(), 1],
                [new Date(1852, 1, 1).valueOf(), 1],
                [new Date(1884, 1, 1).valueOf(), 1],
                [new Date(1907, 1, 1).valueOf(), 1],
                [new Date(1915, 1, 1).valueOf(), 1],
                [new Date(1918, 1, 1).valueOf(), 1],
                [new Date(1919, 1, 1).valueOf(), 1],
                [new Date(1925, 1, 1).valueOf(), 1],
                [new Date(1929, 1, 1).valueOf(), 1],
                [new Date(1930, 1, 1).valueOf(), 1],
                [new Date(1933, 1, 1).valueOf(), 1],
                [new Date(1934, 1, 1).valueOf(), 1],
                [new Date(1935, 1, 1).valueOf(), 1],
                [new Date(1939, 1, 1).valueOf(), 1],
                [new Date(1946, 1, 1).valueOf(), 1],
                [new Date(1948, 1, 1).valueOf(), 1],
                [new Date(1952, 1, 1).valueOf(), 2],
                [new Date(1953, 1, 1).valueOf(), 2],
                [new Date(1954, 1, 1).valueOf(), 1],
                [new Date(1955, 1, 1).valueOf(), 2],
                [new Date(1957, 1, 1).valueOf(), 2],
                [new Date(1959, 1, 1).valueOf(), 1],
                [new Date(1960, 1, 1).valueOf(), 2],
                [new Date(1962, 1, 1).valueOf(), 3],
                [new Date(1964, 1, 1).valueOf(), 1],
                [new Date(1966, 1, 1).valueOf(), 1],
                [new Date(1967, 1, 1).valueOf(), 2],
                [new Date(1968, 1, 1).valueOf(), 1],
                [new Date(1971, 1, 1).valueOf(), 1],
                [new Date(1972, 1, 1).valueOf(), 1],
                [new Date(1973, 1, 1).valueOf(), 1],
                [new Date(1980, 1, 1).valueOf(), 3],
                [new Date(1981, 1, 1).valueOf(), 1],
                [new Date(1984, 1, 1).valueOf(), 1],
                [new Date(1985, 1, 1).valueOf(), 1],
                [new Date(1986, 1, 1).valueOf(), 1],
                [new Date(1987, 1, 1).valueOf(), 1],
                [new Date(1988, 1, 1).valueOf(), 3],
                [new Date(1989, 1, 1).valueOf(), 1],
                [new Date(1990, 1, 1).valueOf(), 2],
                [new Date(1991, 1, 1).valueOf(), 2],
                [new Date(1992, 1, 1).valueOf(), 1],
                [new Date(1995, 1, 1).valueOf(), 3],
                [new Date(1996, 1, 1).valueOf(), 1],
                [new Date(1997, 1, 1).valueOf(), 4],
                [new Date(1998, 1, 1).valueOf(), 2],
                [new Date(2001, 1, 1).valueOf(), 1],
                [new Date(2002, 1, 1).valueOf(), 1],
                [new Date(2003, 1, 1).valueOf(), 2],
                [new Date(2004, 1, 1).valueOf(), 3],
                [new Date(2005, 1, 1).valueOf(), 1],
                [new Date(2006, 1, 1).valueOf(), 1],
                [new Date(2007, 1, 1).valueOf(), 1],
                [new Date(2008, 1, 1).valueOf(), 5],
                [new Date(2009, 1, 1).valueOf(), 11],
                [new Date(2010, 1, 1).valueOf(), 20],
                [new Date(2011, 1, 1).valueOf(), 15],
                [new Date(2012, 1, 1).valueOf(), 16],
                [new Date(2013, 1, 1).valueOf(), 4]
            ]
        }]
    });
});