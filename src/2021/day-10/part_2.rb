file = File.open(File.expand_path('./input.txt', __dir__))
input = file.readlines.map(&:chomp).map { |line| line.split('') }

PAIRS = {
  '[' => ']',
  '(' => ')',
  '{' => '}',
  '<' => '>'
}.freeze

SCORES = {
  ')' => 1,
  ']' => 2,
  '}' => 3,
  '>' => 4
}.freeze

def score_string(str)
  str.reduce(0) { |acc, cur| (acc * 5) + SCORES[cur] }
end

start_chars = PAIRS.keys
incompleted = []

input.each_with_index do |line, _line_idx|
  expected_closers = []
  corrupted = false

  line.each do |char|
    if start_chars.include?(char)
      expected_closers.unshift(PAIRS[char])
    elsif expected_closers[0] != char
      corrupted = true
      break
    else
      expected_closers.shift
    end
  end

  incompleted.push(expected_closers) unless corrupted
end

calculated_scores = incompleted.map { |closers| score_string(closers) }.sort
p calculated_scores[calculated_scores.length / 2]
