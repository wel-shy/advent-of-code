file = File.open("input.txt")
file_data = file.readlines.map(&:chomp)
input = file_data.map { |str| Integer(str) }

count = 0
input.each_with_index do |el, i| 
  if i === 0
    next
  end

  if el > input[i - 1]
    count = count + 1
  end
end

puts count