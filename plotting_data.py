# By submitting this assignment, I agree to the following:
# "Aggies do not lie, cheat, or steal, or tolerate those who do."
# "I have not given or received any unauthorized aid on this assignment."
#
# Name: Hayden Zavala
# Section: 535
# Assignment: Lab Topic 12, Activity 1
# Date: 8th November 2025
#
#
#

import matplotlib.pyplot as mpl
import numpy as np

date_list = []
dew_list = []
pres_list = []
bulb_list = []
wind_list = []
preci_list = []
hum_list = []
ave_temp_list = []
max_tem_list = []
min_tem_list = []
day_num_list = []
month_num_list = []
i = 0
k = 0
#using np.nan to count as using numpy

#prior code to grab WeatherDataCLL below
with open("WeatherDataCLL.csv", "r") as weather_file:
    weather_data_raw = weather_file.read().split("\n")
    weather_data = []
    for element in weather_data_raw[1:]:
        if element:
            weather_data.append(element.split(","))

for day_data in weather_data:
    date = day_data[0]
    date_list.append(date)
    try:
        date_parts = date.split("/")
        month_num = ((int(date_parts[2]) - 2015) * 12 + (int(date_parts[0])))
        month_num_list.append(month_num)
        day_num_list.append(i)
        i += 1
        
    except (ValueError, IndexError):
        month_num_list.append(np.nan) 
        day_num_list.append(i)
        i += 1

    try:
        ave_dew = int(day_data[1])
        dew_list.append(ave_dew)
    except (ValueError, IndexError):
        dew_list.append(np.nan)

    try:
        ave_pres = float(day_data[2])
        pres_list.append(ave_pres)
    except (ValueError, IndexError):
        pres_list.append(np.nan)

    try:
        ave_bulb = int(day_data[3])
        bulb_list.append(ave_bulb)
    except (ValueError, IndexError):
        bulb_list.append(np.nan)

    try:
        ave_wind = float(day_data[4])
        wind_list.append(ave_wind)
    except (ValueError, IndexError):
        wind_list.append(np.nan)

    try:
        preci = float(day_data[5])
        preci_list.append(preci)
    except (ValueError, IndexError):
        preci_list.append(np.nan)

    try:
        ave_hum = int(day_data[6])
        hum_list.append(ave_hum)
    except (ValueError, IndexError):
        hum_list.append(np.nan)

    try:
        ave_temp = int(day_data[7])
        ave_temp_list.append(ave_temp)
    except (ValueError, IndexError):
        ave_temp_list.append(np.nan)

    try:
        max_temp = int(day_data[8])
        max_tem_list.append(max_temp)
    except (ValueError, IndexError):
        max_tem_list.append(np.nan)

    try:
        min_temp = int(day_data[9])
        min_tem_list.append(min_temp)
    except (ValueError, IndexError):
        min_tem_list.append(np.nan)


# Plot 1, Average Wet Bulb Temperature and Average Pressure
mpl.figure(num=1, figsize=(10, 6))
ax1 = mpl.gca()
line1, = ax1.plot(day_num_list, bulb_list, color="red", label="Avg Wet Bulb Temp")
ax1.set_xlabel("date")
ax1.set_ylabel("Average Wet Bulb Temperature (F)", color="red")
ax1.tick_params(axis="y", labelcolor="red")
ax1.set_ylim(10, 85)

ax2 = ax1.twinx() 
line2, = ax2.plot(day_num_list, pres_list, color="blue", label="Avg Pressure")
ax2.set_ylabel("Average Pressure in Hg", color="blue")
ax2.tick_params(axis="y", labelcolor="blue")
ax2.set_ylim(29.1, 30.5)
mpl.title("Average Wet Bulb Temperature and Average Pressure")

lines = [line1, line2]
labels = [l.get_label() for l in lines]
ax1.legend(lines, labels, loc="lower left")


# Plot 2, Histogram of Average Wind Speed
mpl.figure(num=2, figsize=(8, 6))
mpl.hist(wind_list, bins=20, color="green", edgecolor="black")
mpl.title("Histogram of Average Wind Speed")
mpl.xlabel("Average Wind Speed, mph")
mpl.ylabel("Number of days")
mpl.xlim(0,21)
mpl.ylim(0,500) 

# Plot 3, Average Relative Humidity vs Average Dew Point
mpl.figure(num=3, figsize=(7, 6))
mpl.scatter(dew_list, hum_list, marker="o", color="black", s=8)
mpl.title("Average Relative Humidity vs Average Dew Point")
mpl.ylabel("Average Relative Humidity (%)")
mpl.xlabel("Average Dew Point (F)")
mpl.xlim(0, 80)
mpl.ylim(25, 105)


# Plot 4, Temperature and Precipitation by Month 
#dictionary for each of the datas required
monthly_data = {month: {"avg_temps": [], "max_highs": [], "min_lows": [],  "precips": []    } for month in range(1, 13)}
for day_data in weather_data:
    date = day_data[0]
    date_list.append(date) 
    calendar_month = np.nan
    try:
        date_parts = date.split("/")
        calendar_month = int(date_parts[0]) # Month (1-12)
        month_num = ((int(date_parts[2]) - 2015) * 12 + calendar_month)
        month_num_list.append(month_num)
        day_num_list.append(i)
        i += 1
    except (ValueError, IndexError):
        month_num_list.append(np.nan) 
        day_num_list.append(i)
        i += 1
    try:
        ave_temp = int(day_data[7])
        ave_temp_list.append(ave_temp)
    except (ValueError, IndexError):
        ave_temp = np.nan
        ave_temp_list.append(np.nan)
        
    try:
        max_temp = int(day_data[8])
        max_tem_list.append(max_temp)
    except (ValueError, IndexError):
        max_temp = np.nan
        max_tem_list.append(np.nan)

    try:
        min_temp = int(day_data[9])
        min_tem_list.append(min_temp)
    except (ValueError, IndexError):
        min_temp = np.nan
        min_tem_list.append(np.nan)
        
    try:
        preci = float(day_data[5])
    except (ValueError, IndexError):
        preci = np.nan
        #check all values if the have np.nan to make sure we aren't appending nothing to the dictionary
    if not np.isnan(calendar_month):
        month_key = calendar_month 
        if not np.isnan(ave_temp):
            monthly_data[month_key]["avg_temps"].append(ave_temp)
        if not np.isnan(max_temp):
            monthly_data[month_key]["max_highs"].append(max_temp)
        if not np.isnan(min_temp):
            monthly_data[month_key]["min_lows"].append(min_temp)
        if not np.isnan(preci):
            monthly_data[month_key]["precips"].append(preci)
#lists for specific months
months = []
#bar length for graph
mean_avg_temp = []
highest_high_temp = []
lowest_low_temp = []
mean_precip = []

#month_key being in range 
for month_key in range(1, 13):
    data = monthly_data[month_key]
    months.append(month_key)
    mean_avg_temp.append(np.mean(data["avg_temps"]) if data["avg_temps"] else 0)
    highest_high_temp.append(np.max(data["max_highs"]) if data["max_highs"] else np.nan)
    lowest_low_temp.append(np.min(data["min_lows"]) if data["min_lows"] else np.nan)
    mean_precip.append(np.mean(data["precips"]) if data["precips"] else np.nan)


mpl.figure(num=4, figsize=(10, 6))
ax1 = mpl.gca()
ax1.bar(months, mean_avg_temp, width=0.8, color="khaki", label="Mean Avg Temp")
line_high, = ax1.plot(months, highest_high_temp, color="red", label="High T", linewidth=2)
line_low, = ax1.plot(months, lowest_low_temp, color="blue", label="Low T", linewidth=2)
ax1.set_xlabel("Month")
ax1.set_ylabel("Average Temperature, °F", color="black")
ax1.set_ylim(0, 120) 
ax1.set_xticks(months)

ax3 = ax1.twinx() 
line_precip, = ax3.plot(months, mean_precip, color="c", label="Precip", linewidth=1)
ax3.set_ylabel("Monthly Precipitation, in", color="black", rotation=270, labelpad=15)
ax3.tick_params(axis="y")
ax3.set_ylim(0, 1.0) 

mpl.title("Temperature and Precipitation by Month")
lines_all = [line_high, line_low, line_precip]
labels_all = [l.get_label() for l in lines_all]
ax1.legend(lines_all, labels_all, loc="upper left")

#reveal plots in the end
mpl.show()