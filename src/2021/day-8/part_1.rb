file = File.open(File.expand_path('./input.txt', __dir__))
input = file.readlines.map(&:chomp)
input = input.map { |line| line.split(' | ').map { |part| part.split(' ') } }

unique = Hash.new(0)
input.each do |line|
  _, output = line
  output.each do |i|
    chars = i.split('').sort

    unique[1] = unique[1] + 1 if chars.length == 2 # 1
    unique[7] = unique[7] + 1 if chars.length == 3 # 7
    unique[4] = unique[4] + 1 if chars.length == 4 # 4
    unique[8] = unique[8] + 1 if chars.length == 7 # 8
  end
end

puts unique.values.sum
