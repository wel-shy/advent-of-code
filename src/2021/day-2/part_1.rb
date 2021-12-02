file = File.open("input.txt")
input = file.readlines.map(&:chomp)

v = 0
h = 0

input.each do |el|
  command, val = el.split
  val = Integer(val)

  case command
  when "forward"
    h = h + val
  when "up"
    v = v - val
  when "down"
    v = v + val
  end
end

puts v * h