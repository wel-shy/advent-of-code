file = File.open("input.txt")
file_data = file.readlines.map(&:chomp)
input = file_data.map { |str| Integer(str) }

count = 0
sub_arr_len = 3
prev_window_sum = 0
input.each_with_index do |el, i| 
  if i + sub_arr_len > input.length()
    next
  end

  arr = input[i..(i + sub_arr_len - 1)]
  window_sum = arr.sum()

  if i != 0 
    if window_sum > prev_window_sum
      count = count + 1
    end
  end

  prev_window_sum = window_sum
end

puts count