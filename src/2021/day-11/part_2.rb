file = File.open(File.expand_path('./input.txt', __dir__))
input = file.readlines.map(&:chomp).map { |line| line.split('').map { |char| [Integer(char), false] } }

MAX_INTEGER = 2**(0.size * 8 - 2) - 1
MAX_Y = input.length - 1
MAX_X = input[0].length - 1

$grid = input

def valid_coord?(coord)
  x, y = coord
  x >= 0 && x <= MAX_X && y >= 0 && y <= MAX_Y
end

def get_surrounding_values(x, y)
  ul = [x - 1, y - 1]
  up = [x, y - 1]
  ur = [x + 1, y - 1]
  right = [x + 1, y]
  dr = [x + 1, y + 1]
  down = [x, y + 1]
  dl = [x - 1, y + 1]
  left = [x - 1, y]

  [ul, up, ur, right, dr, down, dl, left].select { |coord| valid_coord?(coord) }
end

def increment_values(grid)
  grid.map { |row| row.map { |val| [val[0] + 1, false] } }
end

def increment_neighbours(x, y, grid)
  next_grid = grid

  neighbours = get_surrounding_values(x, y)
  neighbours.each do |nx, ny|
    val, flashed = next_grid[ny][nx]
    next_grid[ny][nx] = [val + 1, flashed]
  end

  next_grid
end

def flash_octopuses(to_flash, grid)
  next_grid = grid
  to_flash.each do |coord|
    x, y = coord
    next_grid[y][x] = [next_grid[y][x][0], true]
    next_grid = increment_neighbours(x, y, next_grid)
  end

  next_grid
end

def calculate_flashes(grid)
  to_flash = []

  grid.each_with_index do |line, y|
    line.each_with_index do |octopus, x|
      val, flashed = octopus
      to_flash.push([x, y]) if !flashed && val > 9
    end
  end

  return grid if to_flash.length.zero?

  grid = flash_octopuses(to_flash, grid)
  calculate_flashes(grid)
end

def synchronised?(grid)
  grid.all? { |row| row.map { |octo| octo[0] }.sum.zero? }
end

step = 0
while step < 1000
  g = increment_values($grid)
  g = calculate_flashes(g)

  g = g.map { |row| row.map { |octopus| [octopus[0] > 9 ? 0 : octopus[0], false] } }

  break if synchronised?(g)

  $grid = g
  step += 1
end

puts step + 1
