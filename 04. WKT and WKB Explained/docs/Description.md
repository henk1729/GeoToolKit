### Introduction
1. WKT: human-understandable, GIS vector data file format, useful for debugging, logging
2. WKB: machine-readable, GIS vector data file format, useful for fast database indexing, network streaming

### File Format
1. WKT:
  * point - `POINT (<longitude> <latitude>)`
    * example - `POINT (73.8567 18.5204)`
  * line - `LINESTRING (<list of longitude and latitude>)`
    * example - `LINESTRING (73.8567 18.5204, 73.8745 18.5312, 73.8912 18.5456)`
  * polygon - `POLYGON ((<list of longitude and latitude>))`
    * example - `POLYGON ((73.81 18.53, 73.89 18.55, 73.85 18.48, 73.81 18.53))`

2. WKB:
  * `<byte order><geometry type><list of concatenated points with their longitude and latitude concatenated in turn>>`
    * example - `0101000000ED9E3C2CD4765240A1D634EF38853240`
