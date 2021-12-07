file = File.open(File.expand_path('./input.txt', __dir__))
input = file.readlines.map(&:chomp)[0].split(',').map { |x| Integer(x) }

day = 0
previous_day = input

while day < 80
  current_day = []
  previous_day.each do |fish|
    if fish.zero?
      current_day.push(8)
      current_day.push(6)
      next
    else
      current_day.push(fish - 1)
    end
  end

  previous_day = current_day
  day += 1
end

puts previous_day.length
