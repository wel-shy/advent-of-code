file = File.open(File.expand_path('./input.txt', __dir__))
input = file.readlines.map(&:chomp)[0].split(',').map { |x| Integer(x) }

input = input.sort

def get_cost(distance)
  return distance if distance.zero?

  ((distance + 1) * distance) / 2
end

cost = 0
position = 0
[*input[0]..input[input.length - 1]].each_with_index do |pos, idx|
  fuel = input.map { |i| get_cost((pos - i).abs) }.sum

  cost = idx.zero? ? fuel : cost
  if fuel < cost
    cost = fuel
    position = pos
  end
end

p cost, position
