file = File.open(File.expand_path('./test.txt', __dir__))
$input = file.readlines.map(&:chomp).map{|line| line.split('').map { |x| Integer(x) }}

$max_integer = 2**(0.size * 8 - 2) - 1
$max_y = $input.length
$max_x = $input[0].length

def get_surrounding_values(x, y)
  up = y.zero? ? $max_integer : $input[y - 1][x]
  down = y == $input.length - 1 ?  $max_integer : $input[y + 1][x]
  left = x.zero? ? $max_integer :  $input[y][x - 1]
  right = x ==  $input[y].length - 1 ? $max_integer : $input[y][x + 1]

  [up, down, left, right]
end

low_points = []
$input.each_with_index do |line, y|
  line.each_with_index do |point, x|
    up, down, left, right = get_surrounding_values(x, y)

    if point < up && point < down && point < left && point < right
      low_points.push([point, x, y])
    end
  end
end

puts low_points.map{|x| x[0] + 1}.sum