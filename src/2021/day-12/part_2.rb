require 'set'

file = File.open(File.expand_path('./input.txt', __dir__))
input = file.readlines.map(&:chomp).map { |line| line.split('-') }

start_nodes = input
              .map { |path| [path[0], []] }
              .concat(input.map { |path| [path[1], []] })
              .to_h

input.each do |cur|
  start, finish = cur
  start_nodes[start] = start_nodes[start].concat([finish])
  start_nodes[finish] = start_nodes[finish].concat([start])
end

def get_paths(nodes, current, path)
  return 1 if current == 'end'

  count = 0
  get_next_nodes(nodes, Array.new(path).concat([current]), current).each do |n|
    count += get_paths(nodes, n, Array.new(path).concat([current]))
  end

  count
end

def get_small_cave_visit_count(path)
  visited = Hash.new(0)
  path.select { |point| point.downcase == point }.each do |point|
    visited[point] += 1
  end

  visited.values.any? { |v| v > 1 }
end

def get_next_nodes(nodes, path, current)
  return [] if current == 'end'
  return path if path.length > 50

  visited_twice = get_small_cave_visit_count(path)

  available = nodes[current]
              .reject { |node| node == 'start' }
              .reject { |node| node.downcase == node && visited_twice ? path.include?(node) : false }

  available.concat([])
end

paths = get_paths(start_nodes, 'start', [])
p paths
