file = File.open("input.txt")
input = file.readlines.map(&:chomp)

aim = 0
v = 0
h = 0

input.each do |el|
  command, val = el.split
  val = Integer(val)

  case command
  when "forward"
    h = h + val
    v =v + (aim * val)
  when "up"
    aim = aim - val
  when "down"
    aim = aim + val
  end
end

puts v * h