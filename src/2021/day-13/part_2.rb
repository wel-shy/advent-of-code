require_relative '../utils/files'

def rotate(sheet, deg)
  rotations = deg / 90
  steps = 0

  new_sheet = sheet
  while steps < rotations
    new_sheet = new_sheet.transpose.map(&:reverse)
    steps += 1
  end

  new_sheet
end

def split_on_y(sheet, y)
  [sheet[0..(y - 1)], sheet[(y + 1)..(sheet.length - 1)]]
end

def split_on_x(sheet, x)
  left = []
  right = []

  sheet.each_with_index do |line, _idx|
    left.push(line[0..(x - 1)])
    right.push(line[(x + 1)..(line.length - 1)])
  end

  [left, right]
end

def overlay_sheets(base, overlay)
  b = base
  overlay.each_with_index do |row, y_idx|
    row.each_with_index do |col, x_idx|
      b[y_idx][x_idx] = overlay[y_idx][x_idx] if col == '#'
    end
  end

  b
end

def fold_on_y(sheet, y)
  top, bottom = split_on_y(sheet, y)
  bottom = rotate(bottom, 180).map(&:reverse)

  overlay_sheets(top, bottom)
end

def fold_on_x(sheet, x)
  left, right = split_on_x(sheet, x)
  right = right.map(&:reverse)

  right.each_with_index do |row, y_idx|
    row.each_with_index do |col, x_idx|
      left[y_idx][x_idx] = right[y_idx][x_idx] if col == '#'
    end
  end

  left
end

input = read_file_as_lines(File.expand_path('./input.txt', __dir__))

coordinates = input.select { |line| line.include? ',' }.map do |coordinate|
  coordinate.split(',').map do |x|
    Integer(x)
  end
end

instructions = input
               .reject { |line| (line.include? ',') || line.empty? }
               .map { |line| line.split(' ')[2] }
               .map { |ins| ins.split('=') }

# Initialise the sheet
max_x = coordinates.map { |coordinate| coordinate[0] }.max
max_y = coordinates.map { |coordinate| coordinate[1] }.max
sheet = (0..max_y).to_a.map { |_row| (0..max_x).to_a.map { |_| '.' } }

coordinates.each do |coord|
  x, y = coord
  sheet[y][x] = '#'
end

ans = sheet
instructions.each do |ins|
  axis, val = ins
  ans = fold_on_x(ans, Integer(val)) if axis == 'x'
  ans = fold_on_y(ans, Integer(val)) if axis == 'y'
end

puts ans.map { |line| line.join('') }.join("\n")
