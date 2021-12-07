file = File.open(File.expand_path('./input.txt', __dir__))
input = file.readlines.map(&:chomp)[0].split(',').map { |x| Integer(x) }

day = 0
previous_day = Hash.new(0)
input.each do |i|
  previous_day[i] += 1
end

while day < 256
  current_day = Hash.new(0)
  previous_day.each do |fish, count|
    if fish.zero?
      current_day[8] += count
      current_day[6] += count
    else
      current_day[fish - 1] += count
    end
  end

  previous_day = current_day
  day += 1
end

puts previous_day.values.sum
