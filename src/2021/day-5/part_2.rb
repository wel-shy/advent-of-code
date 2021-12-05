file = File.open(File.expand_path('./input.txt', __dir__))
input = file.readlines.map(&:chomp)

# Model a line
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

  def is_45_def?
    ((@x1 - @x2).abs) == ((@y1 - @y2).abs)
  end

  def all_coords
    points = []
    x_min, x_max = [@x1, @x2].sort
    y_min, y_max = [@y1, @y2].sort

    points = [*y_min..y_max].map { |y| "#{@x1},#{y}" } if @x1 == @x2
    points = [*x_min..x_max].map { |x| "#{x},#{@y1}" } if @y1 == @y2

    points
  end

  def calc
    dx = @x2 - @x1
    dy = @y2 - @y1
    dir_x = 0
    dir_y = 0
    dir_x = (dx / dx.abs) if dx != 0
    dir_y = (dy / dy.abs) if dy != 0

    _, max_point = [dx.abs, dy.abs].sort

    [*0..max_point].map { |i| "#{@x1 + (i * dir_x)},#{@y1 + (i * dir_y)}" }
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
  coords.push(line)
end

plot = {}
coords.each do |coord|
  if coord.is_45_def?
    coord.calc.each do |point|
      plot[point] = if plot[point]
                      plot[point] + 1
                    else
                      1
                    end
    end
  else
    coord.all_coords.each do |point|
      plot[point] = if plot[point]
                      plot[point] + 1
                    else
                      1
                    end
    end
  end
end

ans = plot.values.reduce(0) { |sum, v| sum + (v > 1 ? 1 : 0) }
puts ans
