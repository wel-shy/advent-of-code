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

p start_nodes

def get_paths(nodes, current, path)
  return 1 if current == 'end'

  count = 0
  get_next_nodes(nodes, Array.new(path.concat([current])), current).each do |n|
    count += get_paths(nodes, n, Array.new(path.concat([current])))
  end

  count
end

def get_next_nodes(nodes, path, current)
  return [] if current == 'end'

  nodes = nodes[current]
          .reject { |node| node == 'start' }
          .reject { |node| node.downcase == node && path.include?(node) }

  nodes.concat([])
end

paths = get_paths(start_nodes, 'start', [])
p paths
