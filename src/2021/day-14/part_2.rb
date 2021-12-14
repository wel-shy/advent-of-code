require 'set'
require_relative '../utils/files'

input = read_file_as_lines(File.expand_path('./input.txt', __dir__))

polymers = input[0].split('')
recipes = Hash.new('')

input[2..(input.length - 1)].each do |recipe|
  i, o = recipe.split(' -> ')
  recipes[i] = o
end

pairs = Hash.new(0)
polymers.each_with_index do |polymer, idx|
  next if idx == polymers.length - 1

  pairs[polymer + polymers[idx + 1]] += 1
end

p_counts = Hash.new(0)
polymers.each do |key|
  p_counts[key] += 1
end

step = 0
while step < 40
  next_pairs = pairs.clone

  recipes.each do |key, val|
    count = pairs[key]
    next unless count.positive?

    a, b = key.split('')
    p_counts[val] += count

    next_pairs[a + val] += count
    next_pairs[a + b] -= count
    next_pairs[val + b] += count
  end

  pairs = next_pairs
  step += 1
end

sorted = p_counts.values.sort
p sorted[sorted.length - 1] - sorted[0]

# # work out each input pair individually, and then add the string
