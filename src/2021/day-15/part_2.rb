require_relative '../utils/files'
require_relative '../utils/arrays'

def get_lowest_f_score(open_set)
  lowest = open_set.values[0]
  open_set.each do |_key, node|
    lowest = node if node[0] < lowest[0]
  end

  lowest
end

def get_h_score(point, finish)
  get_manhattan_distance(
    point.split(',').map { |x| Integer(x) },
    finish.split(',').map { |x| Integer(x) }
  )
end

def expand_grid(grid)
  new_grid = [*(0..(grid.length * 5) - 1)].map { |_line| [*(0..(grid[0].length * 5) - 1)] }
  new_grid.each_with_index do |line, y|
    line.each_with_index do |_point, x|
      next_val = (grid[y % grid.length][x % grid[0].length] + ((x / grid[0].length) + (y / grid.length))) % 9
      new_grid[y][x] = next_val.zero? ? 9 : next_val
    end
  end

  new_grid
end

input = read_file_as_lines(File.expand_path('./input.txt', __dir__))
        .map { |line| line.split('').map { |x| Integer(x) } }

input = expand_grid(input)

MAX_X = input[0].length - 1
MAX_Y = input.length - 1
FINISH = "#{MAX_X},#{MAX_Y}".freeze
START = '0,0'.freeze

graph = Hash.new(Hash.new(0))
input.each_with_index do |line, y|
  line.each_with_index do |_point, x|
    neighbours = get_surrounding_2d_indexes(x, y, MAX_X, MAX_Y)
    n_graph = Hash.new(0)

    neighbours.each do |coord|
      nx, ny = coord
      n_graph[[nx, ny].join(',')] = input[ny][nx]
    end

    graph[[x, y].join(',')] = n_graph
  end
end

# A* search for finding the shorted path, using a queue.
# As we are fetching based on the index of the 2D array
# we can use a regular hashmap instead of a heap and
# benefit from indexing directly.

iter_start = (Time.now.to_f * 1_000).to_i

closed_set = Hash.new(false)
open_set = {}
open_set[START] = [
  0, # f
  0,
  START
]

until open_set.empty?
  current_node = get_lowest_f_score(open_set)
  _f, g, point = current_node
  break if point == FINISH

  open_set.delete(point)

  # filter visited children
  children = graph[point]
  visited_children = children.keys.select { |key| closed_set.key?(key) }
  children = children.reject { |k, _v| visited_children.include?(k) }

  # Add unvisited children to the open set, or adjust it's g score.
  children.each do |key, value|
    child_g = g + value
    child_h = get_h_score(point, FINISH)
    child_node = [child_g + child_h, child_g, key]

    if open_set.key?(key) == false
      open_set[key] = child_node
      next
    end

    open_set[key] = child_node if child_g < open_set[key][1]
  end

  closed_set[point] = true
end

iter_end = (Time.now.to_f * 1_000).to_i

p open_set[FINISH][1]
p "Total time (ms): #{iter_end - iter_start}"
