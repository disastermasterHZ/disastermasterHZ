# By submitting this assignment, I agree to the following:
# "Aggies do not lie, cheat, or steal, or tolerate those who do."
# "I have not given or received any unauthorized aid on this assignment."
#
# Name: Hayden Zavala
# Section: 535
# Assignment: Lab topic 5, boiling curve
# Date: 19th September 2025
#
#
#

import math

#ask for excess temperature
ex_temp = float(input("Enter the excess temperature: "))

#variables to examine where ex_temp lies between

#A point variables
a_xPoint = 1.3
a_yPoint = 1000

#B point variables
b_xPoint = 5
b_yPoint = 7000

#C point variables
c_xPoint = 30
c_yPoint = 1.5 * 1000000

#D point variables
d_xPoint = 120
d_yPoint = 2.5 * 10000

#E point variables
e_xPoint = 1200
e_yPoint = 1.5 * 1000000

#Variables to take on the properties of the variables above for easy calculation
first_xPoint = 0
first_yPoint = 0

second_xPoint = 0
second_yPoint = 0

#variable to activate the print statement
is_valid = False

#check where ex_temp lies between
if not (ex_temp < 0):
    if ex_temp == 0:
        #in case of zero set heat flux to 100 and print the result
        heat_flux = 100
        print(f"The surface heat flux is approximately {heat_flux} W/m^2")
    elif 1.3 <= ex_temp < 5:
        #point a to b
        first_xPoint = a_xPoint
        first_yPoint = a_yPoint
        second_xPoint = b_xPoint
        second_yPoint = b_yPoint
        is_valid = True
    elif 5 <= ex_temp < 30:
        #point b to c
        first_xPoint = b_xPoint
        first_yPoint = b_yPoint
        second_xPoint = c_xPoint
        second_yPoint = c_yPoint
        is_valid = True
    elif 30 <= ex_temp < 120:
        #point c to d
        first_xPoint = c_xPoint
        first_yPoint = c_yPoint
        second_xPoint = d_xPoint
        second_yPoint = d_yPoint
        is_valid = True
    elif 120 <= ex_temp <= 1200:
        #point d to e
        first_xPoint = d_xPoint
        first_yPoint = d_yPoint
        second_xPoint = e_xPoint
        second_yPoint = e_yPoint
        is_valid = True
    elif ex_temp > 1200:
        #too large for boiling curve
        print("Surface heat flux is not available")
else:
    #in case of negative numbers
    print("Surface heat flux is not available")

#check if the ex_temp is a valid input
if is_valid == True:
    #calculate the slope
    slope = (math.log10(second_yPoint / first_yPoint)) / (math.log10(second_xPoint / first_xPoint))
    #calculate the heat_flux
    heat_flux = round(first_yPoint * math.pow((ex_temp/first_xPoint), slope))
    #print the result
    print(f"The surface heat flux is approximately {heat_flux} W/m^2")













