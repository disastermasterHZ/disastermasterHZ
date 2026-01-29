# By submitting this assignment, I agree to the following:
# "Aggies do not lie, cheat, or steal, or tolerate those who do."
# "I have not given or received any unauthorized aid on this assignment."
#
# Name: Hayden Zavala
# Section: 535
# Assignment: Lab Topic 11, Activity 1
# Date: 31st October 2025
#
#
#
barcodes = input("Enter the name of the file: ")
with open(barcodes, "r") as bar_file:
    bar_data = bar_file.read().split("\n")
    valid = 0
    seperator = ""
    #checking for each element in bar_data
    for i in range(len(bar_data)):
        barcode_list = []
        first_group = 0
        second_group = 0
        total_list = []
        for chr in bar_data[i]:
            barcode_list.append(chr)
        else:
            #add to first group
            for k in range(0, len(barcode_list) - 1, 2):
                final_char = int(barcode_list[-1])
                first_group += int(barcode_list[k])
            else:
                #add to second group
                for j in range(1, len(barcode_list), 2):
                    second_group += int(barcode_list[j])
                else:
                    #add together and check result
                    second_group = second_group * 3
                    total_group = first_group + second_group
                    for chr in str(total_group):
                        total_list.append(chr)
                    else:
                        result = 10 - int(total_list[-1])
                        if result == int(final_char):
                            valid += 1
                            with open("valid_barcodes.txt", "a") as valid_file:
                                valid_file.write(seperator.join(barcode_list))
                                valid_file.write("\n")
    print(f"There are {valid} valid barcodes")
                        
                    
                