file = File.open(File.expand_path('./input.txt', __dir__))
input = file.readlines.map(&:chomp).map { |line| line.split('') }

pairs = {
  '[' => ']',
  '(' => ')',
  '{' => '}',
  '<' => '>'
}
scores = {
  ')' => 3,
  ']' => 57,
  '}' => 1197,
  '>' => 25_137
}

start_chars = pairs.keys

corrupted = []
input.each_with_index do |line, _line_idx|
  expected_closers = []

  line.each do |char|
    if start_chars.include?(char)
      expected_closers.unshift(pairs[char])
    elsif expected_closers[0] != char
      corrupted.push(char)
      break
    else
      expected_closers.shift
    end
  end
end

p corrupted.map { |char| scores[char] }.sum
