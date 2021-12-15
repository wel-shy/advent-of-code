require_relative '../utils/files'
require_relative '../utils/arrays'

input = read_file_as_lines(File.expand_path('./input.txt', __dir__))
        .map { |line| line.split('').map { |x| Integer(x) } }

MAX_X = input[0].length - 1
MAX_Y = input.length - 1
FINISH = "#{MAX_X},#{MAX_Y}".freeze

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

def get_lowest_node(weights, visited)
  known_nodes = weights.keys
  known_nodes.reduce(nil) do |lowest, node|
    lowest = node if lowest.nil? && !visited.include?(node)
    lowest = node if weights[node] < weights[lowest] && !visited.include?(node)

    lowest
  end
end

weights = graph['0,0']
parents = {
  '1,0' => '0,0',
  '0,1' => '0,0',
  "#{MAX_X},#{MAX_Y}" => nil?
}
visited = ['0,0']

node = get_lowest_node(weights, visited)
max_iterations = (MAX_X + 1) * (MAX_Y + 1)
iteration = 0
while node && iteration < max_iterations
  p iteration + 1
  weight = weights[node]
  children = graph[node]

  children.each do |k, v|
    new_weight = weight + v
    if weights[k].zero? || new_weight < weights[k]
      weights[k] = new_weight
      parents[k] = node
    end
  end

  visited.push(node)
  node = get_lowest_node(weights, visited)
  iteration += 1
end

p weights[FINISH]
