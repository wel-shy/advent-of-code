# frozen_string_literal: true

file = File.read('./input.txt')
blocks = file.split("\n\n")

# Record for number value and selected status
class BingoNum
  attr_accessor :val, :selected

  def initialize(val)
    @val = val
    @selected = false
  end

  def to_s
    @selected ? 'X' : @val
  end
end

# Track numbers on a card
class Card
  def initialize(rows)
    @rows = rows
  end

  def to_s
    @rows.map { |row| row.map(&:to_s).join(',') }.join("\n")
  end

  def mark_num(val)
    @rows.each do |row|
      row.each do |num|
        num.selected = true if num.val == val
      end
    end
  end

  def complete_row
    @rows.each do |row|
      return true if row.all? { |num| num.selected == true }
    end

    false
  end

  def complete_column
    idx = 0
    while idx < @rows[0].length
      column = @rows.map { |row| row[idx] }

      idx += 1
      return true if column.all?(&:selected)
    end
  end

  def complete
    complete_row || complete_column
  end

  def score(num)
    sum = @rows.reduce(0) { |acc, row| acc + row.map { |n| n.selected ? 0 : n.val }.sum }
    sum * num
  end
end

cards = blocks[1..-1].map { |card| card.split("\n") }.map do |card|
  Card.new(
    card.map do |row|
      row.split(' ').map do |n|
        BingoNum.new(Integer(n))
      end
    end
  )
end
nums = blocks[0].split(',').map { |n| Integer(n) }

completed_cards = []
nums.each do |n|
  break unless cards.each_with_index do |card, idx|
    card.mark_num(n)

    completed_cards.push([idx, card.score(n)]) if card.complete && !completed_cards.map { |c| c[0] }.include?(idx)

    break if completed_cards.length == cards.length
  end
end

puts(completed_cards.map { |el| el.join(', ') })
