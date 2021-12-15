def valid_2d_index?(coord, max_x, max_y)
  x, y = coord
  x >= 0 && x <= max_x && y >= 0 && y <= max_y
end

def get_surrounding_2d_indexes(x, y, max_x, max_y)
  up = [x, y - 1]
  right = [x + 1, y]
  down = [x, y + 1]
  left = [x - 1, y]

  [up, right, down, left].select { |coord| valid_2d_index?(coord, max_x, max_y) }
end

def get_manhattan_distance(start, finish)
  x1, y1 = start
  x2, y2 = finish

  (x2 - x1) + (y2 - y1)
end
