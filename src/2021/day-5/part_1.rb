file = File.open(File.expand_path('./test.txt', __dir__))
input = file.readlines.map(&:chomp)

class Line
  attr_accessor :x1, :x2, :y1, :y2

  def initialize(x1, y1, x2, y2)
    @x1 = Integer(x1)
    @y1 = Integer(y1)
    @x2 = Integer(x2)
    @y2 = Integer(y2)
  end

  def straight?
    @x1 == @x2 || @y1 == @y2
  end

  def all_coords
    points = []
    x_min, x_max = [@x1, @x2].sort
    y_min, y_max = [@y1, @y2].sort

    points = [*y_min..y_max].map { |y| "#{@x1},#{y}" } if @x1 == @x2
    points = [*x_min..x_max].map { |x| "#{x},#{@y1}" } if @y1 == @y2

    points
  end

  def to_s
    "(#{@x1},#{@y1}) => (#{@x2},#{@y2})"
  end
end

coords = []
input.each do |x|
  start, finish = x.split(' -> ')

  x1, y1 = start.split(',')
  x2, y2 = finish.split(',')

  line = Line.new(x1, y1, x2, y2)

  coords.push(line) if line.straight?
end

plot = {}
coords.each do |coord|
  coord.all_coords.each do |point|
    plot[point] = if plot[point]
                    plot[point] + 1
                  else
                    1
                  end
  end
end

ans = plot.values.reduce(0) { |sum, v| sum + (v > 1 ? 1 : 0) }
puts ans

# graph = [*0..9].map { |_v| [*0..9].map { |_x| '.' } }
# plot.each do |k, v|
#   x, y = k.split(',')
#   graph[Integer(y)][Integer(x)] = v
# end

# puts graph.map { |line| line.join(' ') }.join("\n")
