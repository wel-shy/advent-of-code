require 'set'

file = File.open(File.expand_path('./test.txt', __dir__))
input = file.readlines.map(&:chomp)

input = input.map { |line| line.split(' | ').map { |part| part.split(' ') } }

input.each_with_index do |line, _index|
  input, = line
  unique = Hash.new(Set.new([]))
  unknown = Hash.new([])

  input.each do |i|
    chars = i.split('').sort
    unique[1] = Set.new(chars) if chars.length == 2 # 1
    unique[7] = Set.new(chars) if chars.length == 3 # 1
    unique[4] = Set.new(chars) if chars.length == 4 # 4
    unique[8] = Set.new(chars) if chars.length == 7 # 8

    unknown[0] = unknown[0].push(Set.new(chars)) if chars.length == 6
    unknown[9] = unknown[9].push(Set.new(chars)) if chars.length == 6
    # unknown[6] = Set.new(unknown[6].add(chars)) if chars.length == 6
    # unknown[2] = Set.new(unknown[2].add(chars)) if chars.length == 5
    # unknown[3] = Set.new(unknown[3].add(chars)) if chars.length == 5
    # unknown[5] = Set.new(unknown[5].add(chars)) if chars.length == 5
  end

  six = unknown[0].reject { |s| s.superset?(unique[1]) }.to_a[0]
  zero = unknown[0].select { |s| !s.equal?(six) && six.superset?(Set.new(s).subtract(unique[4])) }.to_a[0]
  nine = unknown[0].reject { |s| !s.equal?(six) && six.superset?(Set.new(s).subtract(unique[4])) }.to_a[0]

  a = Set.new(unique[7]).subtract(unique[1])
  c = Set.new(unique[8]).subtract(six)
  f = Set.new(unique[1]).subtract(c)
  d = Set.new(unique[8]).subtract(zero)
  e = Set.new(unique[8]).subtract(nine)
  b = Set.new(unique[4]).subtract(d)

  # puts six, unique[1], a, c, "\n"
  puts zero, _index, "\n"

  # puts unknown
end
