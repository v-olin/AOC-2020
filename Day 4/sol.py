import string


options = {'byr': lambda byr: 1920 <= int(byr) <= 2002,
           'iyr': lambda iyr: 2010 <= int(iyr) <= 2020,
           'eyr': lambda eyr: 2020 <= int(eyr) <= 2030,
           'hgt': lambda hgt: 150 <= int(hgt[:-2]) <= 193 if 'cm' in hgt else 59 <= int(hgt[:-2]) <= 76 if 'in' in hgt else False,
           'hcl': lambda hcl: hcl[0] == '#' and len(hcl[1:]) == 6 and all([h in string.hexdigits for h in hcl[1:]]),
           'ecl': lambda ecl: ecl in ['amb', 'blu', 'brn', 'gry', 'grn', 'hzl', 'oth'],
           'pid': lambda pid: len(pid) == 9 and all([i in string.digits for i in pid])}

part1, part2, pp, lines = 0, 0, {}, [line.strip() for line in open('creds.txt')] + ['']
for line in lines:
    if line == '':
        p1, p2, pp = all([k in pp for k in options]), all([func(pp[k]) for k, func in options.items() if k in pp]), {}
        part1, part2 = part1 + (1 if p1 else 0), part2 + (1 if p1 and p2 else 0)
        continue
    for token in line.split():
        key, value = token.split(':')
        pp[key] = value

print(f"Part 1 Answer: {part1}\nPart 2 Answer: {part2}")