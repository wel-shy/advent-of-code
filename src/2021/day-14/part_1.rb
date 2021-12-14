require_relative '../utils/files'

input = read_file_as_lines(File.expand_path('./input.txt', __dir__))

polymers = input[0].split('')
recipes = Hash.new('')

input[2..(input.length - 1)].each do |recipe|
  i, o = recipe.split(' -> ')
  recipes[i] = o
end

step = 0
while step < 10
  next_polymers = []
  index = 0

  while index < polymers.length - 1
    pair = polymers[index..(index + 1)].join('')
    next_polymers.concat(
      if index.zero?
        [polymers[index], recipes[pair], polymers[index + 1]]
      else
        [recipes[pair], polymers[index + 1]]
      end
    )
    index += 1
  end

  polymers = next_polymers
  step += 1
end

counts = Hash.new(0)
polymers.each do |polymer|
  counts[polymer] += 1
end
counts = counts.values.sort

p counts[counts.length - 1] - counts[0]
