file = File.open("input.txt")
input = file.readlines.map(&:chomp)

lines = input.map{ |i| i.scan /\w/ }
lines = lines.map{|arr| arr.map{|a| Integer(a)}}
lines = lines.transpose.map(&:reverse)
lines = lines.map{|arr| arr.reverse()}

i = 0
bin = []

lines.each_with_index do |line, idx|
  sum = line.sum()
  if sum > ((line.length + 1) / 2)
    bin.push(1)
  else
    bin.push(0)
  end
end

ep = []
bin.each do |el|
  if el > 0
    ep.push(0)
  else
    ep.push(1)
  end
end

puts "gamma #{bin.join(", ")}" 
puts "epsilon #{ep.join(", ")}"

g = bin.join("").to_i(2)
e = ep.join("").to_i(2)
puts g * e