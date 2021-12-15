require_relative '../utils/files'
require_relative '../utils/arrays'

def get_lowest_f_score(open_set)
  lowest = open_set[0]
  open_set.each do |node|
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

open_set = [
  [
    0, # f
    get_h_score(START, FINISH), # h
    0,
    START,
    nil
  ]
]
closed_set = []

step = 0
until open_set.empty?
  step += 1
  current_node = get_lowest_f_score(open_set)
  _, _, g, point, = current_node
  open_set = open_set.reject { |node| node[3] == point }

  p step if (step % 100).zero?

  p g if point == FINISH
  break if point == FINISH

  children = graph[point]
  children.each do |key, value|
    next if closed_set.index { |node| node[3] == key }

    index = open_set.index { |node| node[node.length - 1] == key }
    child_g = g + value
    child_h = get_h_score(point, FINISH)
    child_node = [child_g + child_h, child_h, child_g, key, point]
    if index.nil?
      open_set.push(child_node)
      next
    end

    open_set[index] = [child_node] if child_g < open_set[index][2]
  end

  closed_set.push(current_node)
end
