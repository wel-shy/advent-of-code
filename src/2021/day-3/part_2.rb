file = File.open("./input.txt")
input = file.readlines.map(&:chomp)
input = input.map{ |i| i.scan /\w/ }
input = input.map{|arr| arr.map{|a| Integer(a)}}

def get_column(grid, idx)
  columns = grid.transpose.map(&:reverse)
  columns = columns.map{|arr| arr.reverse()}
  column = columns[idx]
  return column
end

def filter_bin(maj, input)
  row_idx = 0
  col_idx = 0
  col_len = input.length
  remaining = input

  while remaining.length != 1 && col_idx < input[0].length() 
    column = get_column(remaining, col_idx)

    avg = column.inject{ |sum, el| sum + el }.to_f / column.size
    val = avg >= 0.5 ? maj ? 1 : 0 : maj ? 0:  1

    remaining = remaining.select{|col| col[col_idx] == val}

    row_idx = row_idx + 1
    col_idx = col_idx + 1
  end

  oxy = remaining
  return oxy[0].join("").to_i(2)
end

car = filter_bin(false, input)
oxy = filter_bin(true, input)

p car * oxy