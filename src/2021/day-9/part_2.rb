file = File.open(File.expand_path('./input.txt', __dir__))
INPUT = file.readlines.map(&:chomp).map { |line| line.split('').map { |x| Integer(x) } }

MAX_INTEGER = 2**(0.size * 8 - 2) - 1
MAX_Y = INPUT.length
MAX_X = INPUT[0].length

def in_basin?(point, x, y, previous)
  visited = previous.find { |rec| rec[0] == x && rec[1] == y }
  return false if [9, MAX_INTEGER].include?(point)
  return false unless visited.nil?

  true
end

def valid_point?(x, y)
  x >= 0 && x <= MAX_X && y >= 0 && y <= MAX_Y
end

def get_basin_size(point, x, y, record)
  return 0 unless in_basin?(point, x, y, record)

  1 + get_surrounding_values(x, y)
      .select { |val| valid_point?(val[1], val[2]) }
      .map { |val| get_basin_size(val[0], val[1], val[2], record.concat([[x, y]])) }.sum
end

def get_surrounding_values(x, y)
  up = [y.zero? ? MAX_INTEGER : INPUT[y - 1][x], x, y - 1]
  down = [y == INPUT.length - 1 ? MAX_INTEGER : INPUT[y + 1][x], x, y + 1]
  left = [x.zero? ? MAX_INTEGER : INPUT[y][x - 1], x - 1, y]
  right = [x == INPUT[y].length - 1 ? MAX_INTEGER : INPUT[y][x + 1], x + 1, y]

  [up, down, left, right]
end

low_points = []
INPUT.each_with_index do |line, y|
  line.each_with_index do |point, x|
    up, down, left, right = get_surrounding_values(x, y)
    is_low_point = point < up[0] && point < down[0] && point < left[0] && point < right[0]

    low_points.push([point, x, y]) if is_low_point
  end
end

basins = []
low_points.each do |point|
  basins.push(get_basin_size(point[0], point[1], point[2], []))
end
basins = basins.sort.reverse

p basins[0] * basins[1] * basins [2]
