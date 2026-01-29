# By submitting this assignment, I agree to the following:
# "Aggies do not lie, cheat, or steal, or tolerate those who do."
# "I have not given or received any unauthorized aid on this assignment."
#
# Name: Hayden Zavala, Diego Velazquez, Victoria Lopez, David Vazquez
# Section: 535
# Assignment: Team Lab Topic 8,
# Date: 12th October 2025
#
#
#

#asking for the time
time = input("Enter the time: ")

#assigning the values of time based on the digit associated with i
for i in range(1):
    first_char = time[i]
    second_char = time[i+1]
    third_char = time[i+2]
    fourth_char = time[i+3]
    #checking if the size is larger than 4, if so we assign the fifth variable to the fifth character
    if len(time) > 4:
        fifth_char = time[i+4]

# military time (0-24) or regular time (AM to PM)
clock_type = int(input("Choose the clock type (12 or 24): "))

#make sure we don't get an invalid form of time
if clock_type != 12 and clock_type != 24:
    clock_type = int(input("Invalid clock type, choose the clock type (12 or 24): "))
   

#enter a character for each number to represent
prefchar = input("Enter your preferred character: ")
#prefered characters can only come from characters in this string
valid_char = "abcdeghkmnopqrsuvwxyz$@&*="


for chr in prefchar:
    if prefchar == "":
        break
    while prefchar not in valid_char:
        #loop until character is valid or empty
        if prefchar == "":
            break
        prefchar = input("Character not permitted! Try again: ")



# checking if a prefered character was selected or not, if not we default to the regular numbers
if prefchar != "":
    prefchar0 = prefchar
    prefchar1 = prefchar
    prefchar2 = prefchar
    prefchar3 = prefchar
    prefchar4 = prefchar
    prefchar5 = prefchar
    prefchar6 = prefchar
    prefchar7 = prefchar
    prefchar8 = prefchar
    prefchar9 = prefchar
else:
    prefchar0 = "0"
    prefchar1 = "1"
    prefchar2 = "2"
    prefchar3 = "3"
    prefchar4 = "4"
    prefchar5 = "5"
    prefchar6 = "6"
    prefchar7 = "7"
    prefchar8 = "8"
    prefchar9 = "9"




       #dictionaries for each of our numbers
zero = {
    1: prefchar0*3,
    2: prefchar0 + " " + prefchar0,
    3: prefchar0 + " " + prefchar0,
    4: prefchar0 + " " + prefchar0,
    5: prefchar0*3
}




one = {
    1:" " + prefchar1 + " ",
    2:prefchar1*2 + " ",
    3:" " + prefchar1 + " ",
    4:" " + prefchar1 + " ",
    5:prefchar1*3
}



two = {
    1:prefchar2*3,
    2:" " + " " + prefchar2,
    3:prefchar2*3,
    4:prefchar2 + " " + " ",
    5:prefchar2*3
}



three = {
    1:prefchar3*3,
    2:" " + " " + prefchar3,
    3:prefchar3*3,
    4:" " + " " + prefchar3,
    5:prefchar3*3
}



four = {
    1:prefchar4 + " " + prefchar4,
    2:prefchar4 + " " + prefchar4,
    3:prefchar4*3,
    4:" " + " " + prefchar4,
    5:" " + " " + prefchar4,
}



five = {
    1:prefchar5*3,
    2:prefchar5 + " " + " ",
    3:prefchar5*3,
    4:" " + " " + prefchar5,
    5:prefchar5*3
}



six = {
    1:prefchar6*3,
    2:prefchar6 + " " + " ",
    3:prefchar6*3,
    4:prefchar6 + " " + prefchar6,
    5:prefchar6*3
}



seven = {
    1:prefchar7*3,
    2:" " + " " + prefchar7,
    3:" " + " " + prefchar7,
    4:" " + " " + prefchar7,
    5:" " + " " + prefchar7,
}



eight = {
    1:prefchar8*3,
    2:prefchar8 + " " + prefchar8,
    3:prefchar8*3,
    4:prefchar8 + " " + prefchar8,
    5:prefchar8*3,
}



nine = {
    1:prefchar9*3,
    2:prefchar9 + " " + prefchar9,
    3:prefchar9*3,
    4:" " + " " + prefchar9,
    5:prefchar9*3
}





A = {
    1:" " + "A" + " ",
    2:"A" + " " + "A",
    3:"A"*3,
    4:"A" + " " + "A",
    5:"A" + " " + "A",
}




P = {
    1:"P"*3,
    2:"P" + " " + "P",
    3:"P"*3,
    4:"P" + " " + " ",
    5:"P" + " " + " ",
}




M = {
    1:"M" + " " + " " + " " + "M",
    2:"M" + "M" + " " + "M" + "M",
    3:"M" + " " + "M" + " " + "M",
    4:"M" + " " + " " + " " + "M",
    5:"M" + " " + " " + " " + "M",
}




colon = {
    1:" "*3,
    2:" " + ":" + " ",
    3:" "*3,
    4:" " + ":" + " ",
    5:" "*3
}




      #  testing numbers
#print(one[1], colon[1], sep = " ")
#print(one[2], colon[2], sep = " ")
#print(one[3], colon[3], sep = " ")
#print(one[4], colon[4], sep = " ")
#print(one[5], colon[5], sep = " ")



numchar = {"0": zero, "1": one, "2": two, "3": three, "4": four, "5": five, "6": six, "7": seven, "8": eight, "9": nine, ":": colon}

#to seperate from the input above
print("")   

#check the length of the string to determine the size of the time listed.
if len(time) > 4:
    #military time, therefore there is no real need to worry
    if clock_type == 24:
        #we loop 5 times to print all 5 seperate lines
        for i in range(1, 6):
            print(f"{numchar[first_char][i]} {numchar[second_char][i]}{numchar[third_char][i]}{numchar[fourth_char][i]} {numchar[fifth_char][i]}")
    else:
        #if listed at 22:XX or above to convert back to 10 - 12 depending on size
        if int(first_char + second_char) >= 22:
            #for the first character to be 1 to fit in the range of 1:00 - 12:00 
            first_char = '1'
            # Have the second character be it's number reduced by -2 (4 - 2 = 2)
            second_char = str(int(second_char) + 10 - 12)
            for i in range(1, 6):
                print(f"{numchar[first_char][i]} {numchar[second_char][i]}{numchar[third_char][i]}{numchar[fourth_char][i]} {numchar[fifth_char][i]} {P[i]} {M[i]}")
        elif int(second_char) > 2:
            #for anything above 12:XX (18:XX = 8 - 2 = 6 therfore 6:XX timeframe) 
           first_char = str(int(second_char) + 10 - 12)
           for i in range(1, 6):
               print(f"{numchar[first_char][i]}{numchar[third_char][i]}{numchar[fourth_char][i]} {numchar[fifth_char][i]} {P[i]} {M[i]}")
        elif int(second_char) == 2:
            #We make this PM for hitting the afternoon
            for i in range(1, 6):
                print(f"{numchar[first_char][i]} {numchar[second_char][i]}{numchar[third_char][i]}{numchar[fourth_char][i]} {numchar[fifth_char][i]} {P[i]} {M[i]}")
        else:
            #Before the afternoon (AM)
            for i in range(1, 6):
                print(f"{numchar[first_char][i]} {numchar[second_char][i]}{numchar[third_char][i]}{numchar[fourth_char][i]} {numchar[fifth_char][i]} {A[i]} {M[i]}")
           
else:
    #Military time, no need to convert to 12:00 A.M
    if clock_type == 24:
        for i in range(1, 6):
            print(f"{numchar[first_char][i]}{numchar[second_char][i]}{numchar[third_char][i]} {numchar[fourth_char][i]}")
    else:
        #Incase 0:XX is picked, we default to 12:XX A.M
        if first_char == "0":
            first_char = "1"
            second_char = "2"
            fifth_char = fourth_char
            fourth_char = third_char
            third_char = ":"
            for i in range(1, 6):
                print(f"{numchar[first_char][i]} {numchar[second_char][i]}{numchar[third_char][i]}{numchar[fourth_char][i]} {numchar[fifth_char][i]} {A[i]} {M[i]}")
                
        else:
            #All other events less than 10:00 and not starting with zero when using 12 time.
            for i in range(1, 6):
                print(f"{numchar[first_char][i]}{numchar[second_char][i]}{numchar[third_char][i]} {numchar[fourth_char][i]} {A[i]} {M[i]}")












